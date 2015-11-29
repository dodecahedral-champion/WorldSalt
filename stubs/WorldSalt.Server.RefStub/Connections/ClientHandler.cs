namespace WorldSalt.Server.RefStub.Connections {
	using System;
	using System.Collections.Generic;
	using System.Linq;
	using System.Net.Sockets;
	using WorldSalt.Network;
	using WorldSalt.Network.Direction;
	using WorldSalt.Network.Packets;
	using WorldSalt.Network.Packets.Connection;
	using WorldSalt.Network.Streams;

	public class ClientHandler : IClientHandler {
		IStreamDuplex<ITypedPacket<FromServer>, ITypedPacket<FromClient>> stream;
		IPacketFactory<FromServer> packetFactory;
		const UInt64 SERVER_PROTOCOL = ProtocolVersion.CURRENT;
		string username;

		public ClientHandler(IPacketFactory<FromServer> packetFactory, IStreamDuplex<ITypedPacket<FromServer>, ITypedPacket<FromClient>> stream) {
			this.packetFactory = packetFactory;
			this.stream = stream;
			username = Guid.NewGuid().ToString();
		}

		public void Run() {
			var connectPacket = stream.Take();
			var connectPayload = connectPacket.Payload as ConnectPayload;
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
			stream.Put(packetFactory.Create(new ConnectedPayload()));
		}

		private void RejectConnection() {
			Log("rejected connection");
			stream.Put(packetFactory.Create(new UnsupportedProtocolVersionPayload(SERVER_PROTOCOL, Enumerable.Empty<UInt64>())));
			stream.Close();
		}

		private void DisconnectClient() {
			Log("kicked client");
			stream.Put(packetFactory.Create(new KickedPayload("you're done here")));
			stream.Close();
		}

		private bool IsVersionOkay(UInt64 requestedVersion, IEnumerable<UInt64> alternateVersions) {
			return requestedVersion == SERVER_PROTOCOL || alternateVersions.Contains(SERVER_PROTOCOL);
		}

		private void Log(string format, params object[] arguments) {
			Console.WriteLine("[server:{0}] {1}", username, string.Format(format, arguments));
		}
	}
}
