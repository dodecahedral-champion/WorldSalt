namespace WorldSalt.Network.Payloads.Connection {
	using WorldSalt.Network.Direction;
	using WorldSalt.Network.Streams.Bytes;

	public abstract class BaseTypedPayload<TDir> : ITypedPayload<TDir> where TDir : IDirection {
		public abstract byte Type { get; }
		public abstract byte Subtype { get; }
		public abstract uint Length { get; }

		public abstract void SetBytes(byte[] bytes);
		public abstract byte[] GetBytes();

		protected IByteSource<TDir> MakeDirectedByteStream(byte[] bytes) {
			return new ByteArrayStream<TDir>(bytes);
		}
	}
}
