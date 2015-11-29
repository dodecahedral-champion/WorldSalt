namespace WorldSalt.Network.Streams {
	using System.Net.Sockets;
	using WorldSalt.Network.Direction;
	using WorldSalt.Network.Frames;
	using WorldSalt.Network.Payloads;
	using WorldSalt.Network.Streams.Bytes;

	public class FrameStreamFactory : IFrameStreamFactory {
		public IStreamDuplex<ITypedFrame<FromServer>, ITypedFrame<FromClient>> CreateDuplexForServer(TcpClient socket) {
			var payloadFactory = new PayloadFactory<FromClient>(new PayloadTypedCreatorFromClient());
			var frameFactory = new FrameFactory<FromClient>(payloadFactory);
			socket.NoDelay = true;
			var byteSource = new TcpByteSource<FromClient>(socket);
			var byteSink = new TcpByteSink<FromServer>(socket);
			return new FrameStream<FromServer, FromClient>(byteSource, byteSink, frameFactory, payloadFactory);
		}

		public IStreamDuplex<ITypedFrame<FromClient>, ITypedFrame<FromServer>> CreateDuplexForClient(TcpClient socket) {
			var payloadFactory = new PayloadFactory<FromServer>(new PayloadTypedCreatorFromServer());
			var frameFactory = new FrameFactory<FromServer>(payloadFactory);
			socket.NoDelay = true;
			var byteSource = new TcpByteSource<FromServer>(socket);
			var byteSink = new TcpByteSink<FromClient>(socket);
			return new FrameStream<FromClient, FromServer>(byteSource, byteSink, frameFactory, payloadFactory);
		}
	}
}
