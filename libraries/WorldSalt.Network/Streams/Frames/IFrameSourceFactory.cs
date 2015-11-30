namespace WorldSalt.Network.Streams.Frames {
	using WorldSalt.Network.Direction;
	using WorldSalt.Network.Frames;
	using WorldSalt.Network.Streams.Bytes;

	public interface IFrameSourceFactory<TDir> where TDir : IDirection {
		IStreamProducer<ITypedFrame<TDir>> Create(IByteSource<TDir> byteSource);
	}
}
