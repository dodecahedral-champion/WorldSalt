namespace WorldSalt.Server.RefStub.Connections {
	using System.Net;
	using System.Net.Sockets;

	public class ConnectionFoyer : IConnectionFoyer {
		private TcpListener listener;
		IClientHandlerFactory clientHandlerFactory;

		public ConnectionFoyer(IClientHandlerFactory clientHandlerFactory, int port) {
			this.clientHandlerFactory = clientHandlerFactory;
			listener = new TcpListener(IPAddress.Any, port);
			listener.Start();
		}

		public void Dispose() {
			listener.Stop();
		}

		public IClientHandler AcceptOne() {
			return clientHandlerFactory.Create(listener.AcceptTcpClient());
		}
	}
}
