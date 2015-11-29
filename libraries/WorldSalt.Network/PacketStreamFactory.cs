namespace WorldSalt.Network {
	using System.Net.Sockets;

	public class PacketStreamFactory : IPacketStreamFactory {
		public IStreamDuplex<IPacket> CreateDuplex(TcpClient socket) {
			throw new System.NotImplementedException();
		}
	}
}
