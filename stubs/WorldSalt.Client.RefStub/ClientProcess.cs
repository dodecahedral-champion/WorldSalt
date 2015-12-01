namespace WorldSalt.Client.RefStub {
	using System;
	using System.Linq;
	using System.Net.Sockets;
	using WorldSalt.Network.Direction;
	using WorldSalt.Network.Payloads;
	using WorldSalt.Network.Payloads.Connection;
	using WorldSalt.Network.Streams;
	using WorldSalt.Network.Streams.Bytes;
	using WorldSalt.Network.Streams.Payloads;

	public class ClientProcess : IDisposable {
		private readonly IStreamConsumer<ITypedPayload<FromClient>> payloadSink;
		private readonly IStreamProducer<ITypedPayload<FromServer>> payloadSource;

		public ClientProcess(IPayloadSinkFactory<FromClient> sinkFactory, IPayloadSourceFactory<FromServer> sourceFactory, string hostname, int port) {
			var socket = new TcpClient(hostname, port);
			var byteSink = new TcpByteSink<FromClient>(socket);
			var byteSource = new TcpByteSource<FromServer>(socket);
			payloadSink = sinkFactory.Create(byteSink);
			payloadSource = sourceFactory.Create(byteSource);
		}

		public void Dispose() {
			Close();
			payloadSink.Dispose();
			payloadSource.Dispose();
		}

		public void Close() {
			payloadSink.Close();
			payloadSource.Close();
		}

		public void Connect(string username, UInt64 protocolVersion) {
			Console.WriteLine("[client] connecting...");
			payloadSink.Put(new ConnectPayload(username, protocolVersion, Enumerable.Empty<UInt64>()));
			var connectResponse = payloadSource.Take();
			CheckForUnsupportedVersion(connectResponse as UnsupportedProtocolVersionPayload);
			if(connectResponse is ConnectedPayload) {
				Console.WriteLine("[client] connected okay!");
			}
		}

		public void Disconnect() {
			Console.WriteLine("[client] disconnecting.");
			payloadSink.Put(new DisconnectPayload());
			Close();
		}

		private void CheckForUnsupportedVersion(UnsupportedProtocolVersionPayload payload) {
			if(payload == null) {
				return;
			}

			var message = string.Format("preferred protocol {0}", payload.PreferredProtocol);
			if (payload.SupportedProtocols.Any()) {
				message = string.Format("{0} (or any of: {1})", message, string.Join(",", payload.SupportedProtocols.Select(x => x.ToString())));
			}
			Console.WriteLine("[client] rejected: {0}", message);

			throw new InvalidOperationException(message);
		}
	}
}
