namespace WorldSalt.Server.RefStub.Connections {
	using System.Net.Sockets;
	using WorldSalt.Network;
	using WorldSalt.Network.Direction;
	using WorldSalt.Network.Packets;

	public class ClientHandlerFactory : IClientHandlerFactory {
		private IPacketFactory<FromServer> packetFactory;
		private IPacketStreamFactory packetStreamFactory;

		public ClientHandlerFactory(IPacketFactory<FromServer> packetFactory, IPacketStreamFactory packetStreamFactory) {
			this.packetFactory = packetFactory;
			this.packetStreamFactory = packetStreamFactory;
		}

		public IClientHandler Create(TcpClient tcpClient) {
			var stream = packetStreamFactory.CreateDuplexForServer(tcpClient);
			return new ClientHandler(packetFactory, stream);
		}
	}
}
