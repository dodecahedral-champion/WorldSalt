namespace WorldSalt.Server.RefStub.Connections {
	using System.Net.Sockets;
	using WorldSalt.Network;
	using WorldSalt.Network.Packets;

	public class ClientHandler : IClientHandler {
		public ClientHandler(IStreamDuplex<IPacketFromServer, IPacketFromClient> stream) {
		}

		public void Run() {
			throw new System.NotImplementedException();
		}
	}
}
