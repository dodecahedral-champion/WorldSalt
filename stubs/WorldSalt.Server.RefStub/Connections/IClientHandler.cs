namespace WorldSalt.Server.RefStub.Connections {
	using System;

	public interface IClientHandler : IDisposable {
		void Run();
		void Close();
	}
}
