namespace WorldSalt.Network {
	using System.Net.Sockets;

	public interface IPacketStreamFactory {
		IStreamDuplex<IPacket> CreateDuplex(TcpClient socket);
	}
}
