namespace WorldSalt.Network {
	using System.Net.Sockets;
	using WorldSalt.Network.Direction;
	using WorldSalt.Network.Packets;

	public class PacketStreamFactory : IPacketStreamFactory {
		public IStreamDuplex<ITypedPacket<FromServer>, ITypedPacket<FromClient>> CreateDuplexForServer(TcpClient socket) {
			throw new System.NotImplementedException();
		}
	}
}
