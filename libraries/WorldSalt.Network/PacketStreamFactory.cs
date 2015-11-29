namespace WorldSalt.Network {
	using System.Net.Sockets;
	using WorldSalt.Network.Packets;

	public class PacketStreamFactory : IPacketStreamFactory {
		public IStreamDuplex<IPacketFromServer, IPacketFromClient> CreateDuplexForServer(TcpClient socket) {
			throw new System.NotImplementedException();
		}
	}
}
