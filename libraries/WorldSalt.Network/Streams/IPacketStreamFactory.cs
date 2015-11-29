namespace WorldSalt.Network.Streams {
	using System.Net.Sockets;
	using WorldSalt.Network.Direction;
	using WorldSalt.Network.Packets;

	public interface IPacketStreamFactory {
		IStreamDuplex<ITypedPacket<FromServer>, ITypedPacket<FromClient>> CreateDuplexForServer(TcpClient socket);
		IStreamDuplex<ITypedPacket<FromClient>, ITypedPacket<FromServer>> CreateDuplexForClient(TcpClient socket);
	}
}
