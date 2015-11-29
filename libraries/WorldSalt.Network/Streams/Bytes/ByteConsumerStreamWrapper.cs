namespace WorldSalt.Network.Streams.Bytes {
	using System.IO;
	using WorldSalt.Network.Direction;

	public class ByteConsumerStreamWrapper<TDirection> : IByteSink<TDirection> where TDirection : IDirection {
		protected Stream underlying;

		public ByteConsumerStreamWrapper(Stream underlying) {
			this.underlying = underlying;
		}

		public void Dispose() {
			underlying.Close();
			underlying.Dispose();
		}

		public void Close() {
			underlying.Close();
		}

		public void Put(byte item) {
			underlying.WriteByte(item);
		}

		public void Put(byte[] bytes) {
			underlying.Write(bytes, 0, bytes.Length);
		}
	}
}
