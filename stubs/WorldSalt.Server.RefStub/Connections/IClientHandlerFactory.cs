namespace WorldSalt.Server.RefStub.Connections {
	using System.Net.Sockets;

	public interface IClientHandlerFactory {
		IClientHandler Create(TcpClient tcpClient);
	}
}
