namespace WorldSalt.Network.Streams {
	using System.Net.Sockets;
	using WorldSalt.Network.Direction;
	using WorldSalt.Network.Frames;
	using WorldSalt.Network.Payloads;

	public class FrameStreamFactory : IFrameStreamFactory {
		public IStreamDuplex<ITypedFrame<FromServer>, ITypedFrame<FromClient>> CreateDuplexForServer(TcpClient socket) {
			var payloadFactory = new PayloadFactory<FromClient>(new PayloadTypedCreatorFromClient());
			var frameFactory = new FrameFactory<FromClient>(payloadFactory);
			return new FrameStream<FromServer, FromClient>(socket, frameFactory, payloadFactory);
		}

		public IStreamDuplex<ITypedFrame<FromClient>, ITypedFrame<FromServer>> CreateDuplexForClient(TcpClient socket) {
			var payloadFactory = new PayloadFactory<FromServer>(new PayloadTypedCreatorFromServer());
			var frameFactory = new FrameFactory<FromServer>(payloadFactory);
			return new FrameStream<FromClient, FromServer>(socket, frameFactory, payloadFactory);
		}
	}
}
