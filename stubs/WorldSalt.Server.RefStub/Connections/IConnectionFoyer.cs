namespace WorldSalt.Server.RefStub.Connections {
	using System;

	public interface IConnectionFoyer : IDisposable {
		IClientHandler AcceptOne();
	}
}
