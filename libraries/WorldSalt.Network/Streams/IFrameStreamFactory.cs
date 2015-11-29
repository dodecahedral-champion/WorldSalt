namespace WorldSalt.Network.Streams {
	using System.Net.Sockets;
	using WorldSalt.Network.Direction;
	using WorldSalt.Network.Frames;

	public interface IFrameStreamFactory {
		IStreamDuplex<ITypedFrame<FromServer>, ITypedFrame<FromClient>> CreateDuplexForServer(TcpClient socket);
		IStreamDuplex<ITypedFrame<FromClient>, ITypedFrame<FromServer>> CreateDuplexForClient(TcpClient socket);
	}
}
