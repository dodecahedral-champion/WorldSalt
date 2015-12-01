namespace WorldSalt.Server.RefStub.Connections {
	using System;
	using System.Collections.Generic;
	using System.Linq;
	using WorldSalt.Network;
	using WorldSalt.Network.Direction;
	using WorldSalt.Network.Payloads;
	using WorldSalt.Network.Payloads.Connection;
	using WorldSalt.Network.Streams;

	public class ClientHandler : IClientHandler {
		private readonly IStreamConsumer<ITypedPayload<FromServer>> payloadSink;
		private readonly IStreamProducer<ITypedPayload<FromClient>> payloadSource;
		const UInt64 SERVER_PROTOCOL = ProtocolVersion.CURRENT;
		string username;

		public ClientHandler(IStreamConsumer<ITypedPayload<FromServer>> payloadSink, IStreamProducer<ITypedPayload<FromClient>> payloadSource) {
			this.payloadSink = payloadSink;
			this.payloadSource = payloadSource;
			username = Guid.NewGuid().ToString();
		}

		public void Dispose() {
			Close();
			payloadSource.Dispose();
			payloadSink.Dispose();
		}

		public void Close() {
			payloadSource.Close();
			payloadSink.Close();
		}

		public void Run() {
			var connectPayload = payloadSource.Take() as ConnectPayload;
			if (connectPayload == null) {
				Log("bad connect packet");
				throw new InvalidOperationException("bad connect packet");
			}

			Log("connecting user [[{0}]]", connectPayload.Username);
			username = connectPayload.Username;

			if (!IsVersionOkay(connectPayload.PreferredProtocol, connectPayload.SupportedProtocols)) {
				RejectConnection();
				return;
			}

			AcceptConnection();
			DisconnectClient();
		}

		private void AcceptConnection() {
			Log("accepted connection");
			payloadSink.Put(new ConnectedPayload());
		}

		private void RejectConnection() {
			Log("rejected connection");
			payloadSink.Put(new UnsupportedProtocolVersionPayload(SERVER_PROTOCOL, Enumerable.Empty<UInt64>()));
			Close();
		}

		private void DisconnectClient() {
			Log("kicked client");
			payloadSink.Put(new KickedPayload("you're done here"));
			Close();
		}

		private bool IsVersionOkay(UInt64 requestedVersion, IEnumerable<UInt64> alternateVersions) {
			return requestedVersion == SERVER_PROTOCOL || alternateVersions.Contains(SERVER_PROTOCOL);
		}

		private void Log(string format, params object[] arguments) {
			Console.WriteLine("[server:{0}] {1}", username, string.Format(format, arguments));
		}
	}
}
