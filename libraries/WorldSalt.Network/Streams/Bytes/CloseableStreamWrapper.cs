namespace WorldSalt.Network.Streams.Bytes {
	using System.IO;

	public class CloseableStreamWrapper : IStreamCloseable {
		private Stream underlying;

		public CloseableStreamWrapper(Stream underlying) {
			this.underlying = underlying;
		}

		public void Close() {
			underlying.Close();
		}

		public void Dispose() {
			underlying.Close();
			underlying.Dispose();
		}
	}
}

