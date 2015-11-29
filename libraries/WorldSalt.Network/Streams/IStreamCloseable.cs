namespace WorldSalt.Network.Streams {
	using System;

	public interface IStreamCloseable : IDisposable {
		void Close();
	}
}
