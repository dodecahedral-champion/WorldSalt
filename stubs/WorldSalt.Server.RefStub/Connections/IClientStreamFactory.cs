namespace WorldSalt.Server.RefStub.Connections {
	using System.Net.Sockets;

	public interface IClientStreamFactory {
		void Create(TcpClient tcpClient);
	}
}
