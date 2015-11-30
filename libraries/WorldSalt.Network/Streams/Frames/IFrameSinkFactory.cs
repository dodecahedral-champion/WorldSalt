namespace WorldSalt.Network.Streams.Frames {
	using WorldSalt.Network.Direction;
	using WorldSalt.Network.Frames;
	using WorldSalt.Network.Streams.Bytes;

	public interface IFrameSinkFactory<TDir> where TDir : IDirection {
		IStreamConsumer<ITypedFrame<TDir>> Create(IByteSink<TDir> byteSink);
	}
}
