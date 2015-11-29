namespace WorldSalt.Network.Streams.Bytes {
	using System.IO;
	using WorldSalt.Network.Direction;

	public class ByteArrayStream<TDir> : ByteProducerStreamWrapper<TDir> where TDir : IDirection {
		public ByteArrayStream(byte[] array) : base(new MemoryStream(array)) {
		}
	}
}
