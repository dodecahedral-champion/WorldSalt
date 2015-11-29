namespace WorldSalt.Network {
	using System.Net.Sockets;
	using WorldSalt.Network.Packets;

	public interface IPacketStreamFactory {
		IStreamDuplex<IPacketFromServer, IPacketFromClient> CreateDuplexForServer(TcpClient socket);
	}
}
