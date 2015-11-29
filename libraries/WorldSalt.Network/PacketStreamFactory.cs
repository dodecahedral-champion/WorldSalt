namespace WorldSalt.Network {
	using System.Net.Sockets;
	using WorldSalt.Network.Direction;
	using WorldSalt.Network.Packets;

	public class PacketStreamFactory : IPacketStreamFactory {
		public IStreamDuplex<ITypedPacket<FromServer>, ITypedPacket<FromClient>> CreateDuplexForServer(TcpClient socket) {
			var payloadFactory = new PayloadFactory<FromClient>(new PayloadTypedCreatorFromClient());
			var packetFactory = new PacketFactory<FromClient>(payloadFactory);
			return new PacketStream<FromServer, FromClient>(socket, packetFactory, payloadFactory);
		}

		public IStreamDuplex<ITypedPacket<FromClient>, ITypedPacket<FromServer>> CreateDuplexForClient(TcpClient socket) {
			var payloadFactory = new PayloadFactory<FromServer>(new PayloadTypedCreatorFromServer());
			var packetFactory = new PacketFactory<FromServer>(payloadFactory);
			return new PacketStream<FromClient, FromServer>(socket, packetFactory, payloadFactory);
		}
	}
}
