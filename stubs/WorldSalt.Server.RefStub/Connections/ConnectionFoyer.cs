namespace WorldSalt.Server.RefStub.Connections {
	using System;
	using System.Net;
	using System.Net.Sockets;
	using WorldSalt.Network.Direction;
	using WorldSalt.Network.Streams.Bytes;

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
			var socket = listener.AcceptTcpClient();
			Console.WriteLine("[server] met new client");
			var byteSink = new TcpByteSink<FromServer>(socket);
			var byteSource = new TcpByteSource<FromClient>(socket);
			return clientHandlerFactory.Create(byteSink, byteSource);
		}
	}
}
