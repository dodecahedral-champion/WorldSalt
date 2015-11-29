namespace WorldSalt.Network.Streams.Bytes {
	using WorldSalt.Network.Direction;

	public interface IByteDuplex<TConsumeDir, TProduceDir> : IStreamCloseable, IByteSink<TConsumeDir>, IByteSource<TProduceDir> where TConsumeDir : IDirection where TProduceDir : IDirection {
	}
}

