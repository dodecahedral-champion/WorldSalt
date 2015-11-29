namespace WorldSalt.Server.RefStub.Connections {
	using System.Net.Sockets;
	using WorldSalt.Network;

	public class ClientHandlerFactory : IClientHandlerFactory {
		private IPacketStreamFactory packetStreamFactory;

		public ClientHandlerFactory(IPacketStreamFactory packetStreamFactory) {
			this.packetStreamFactory = packetStreamFactory;
		}

		public IClientHandler Create(TcpClient tcpClient) {
			var stream = packetStreamFactory.CreateDuplex(tcpClient);
			return new ClientHandler(stream);
		}
	}
}
