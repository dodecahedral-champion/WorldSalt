namespace WorldSalt.Server.RefStub.Connections {
	using System.Net.Sockets;
	using WorldSalt.Network;

	public class ClientHandler : IClientHandler {
		public ClientHandler(IStreamDuplex<IPacket> stream) {
		}

		public void Run() {
			throw new System.NotImplementedException();
		}
	}
}
