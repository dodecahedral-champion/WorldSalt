namespace WorldSalt.Server.RefStub.Connections {
	using System.Net.Sockets;
	using WorldSalt.Network;
	using WorldSalt.Network.Direction;
	using WorldSalt.Network.Packets;

	public class ClientHandler : IClientHandler {
		public ClientHandler(IStreamDuplex<ITypedPacket<FromServer>, ITypedPacket<FromClient>> stream) {
		}

		public void Run() {
			throw new System.NotImplementedException();
		}
	}
}
