using System.Linq;

namespace WorldSalt.Client.RefStub {
	using System;
	using System.Net.Sockets;
	using WorldSalt.Network;
	using WorldSalt.Network.Direction;
	using WorldSalt.Network.Packets;
	using WorldSalt.Network.Packets.Connection;

	public class ClientProcess {
		private IStreamDuplex<ITypedPacket<FromClient>, ITypedPacket<FromServer>> stream;
		public ClientProcess(IPacketStreamFactory packetStreamFactory, string hostname, int port) {
			stream = packetStreamFactory.CreateDuplexForClient(new TcpClient(hostname, port));
		}

		public void Run() {
			Connect("GreyKnight", ProtocolVersion.CURRENT);
		}

		private void Connect(string username, UInt64 protocolVersion) {
			var connectPacket = new Packet<ConnectPayload, FromClient>(new ConnectPayload(username, protocolVersion, Enumerable.Empty<UInt64>()));
			stream.Put(connectPacket);
		}
	}
}
