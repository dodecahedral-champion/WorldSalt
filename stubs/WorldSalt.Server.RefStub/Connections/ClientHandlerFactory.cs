namespace WorldSalt.Server.RefStub.Connections {
	using System.Net.Sockets;
	using WorldSalt.Network;
	using WorldSalt.Network.Direction;
	using WorldSalt.Network.Frames;
	using WorldSalt.Network.Streams;

	public class ClientHandlerFactory : IClientHandlerFactory {
		private IFrameFactory<FromServer> frameFactory;
		private IFrameStreamFactory streamFactory;

		public ClientHandlerFactory(IFrameFactory<FromServer> frameFactory, IFrameStreamFactory streamFactory) {
			this.frameFactory = frameFactory;
			this.streamFactory = streamFactory;
		}

		public IClientHandler Create(TcpClient tcpClient) {
			var stream = streamFactory.CreateDuplexForServer(tcpClient);
			return new ClientHandler(frameFactory, stream);
		}
	}
}
