namespace WorldSalt.Network.Streams.Frames {
	using WorldSalt.Network.Direction;
	using WorldSalt.Network.Frames;
	using WorldSalt.Network.Payloads;
	using WorldSalt.Network.Streams.Bytes;

	public class FrameSinkFactory<TDir> : IFrameSinkFactory<TDir> where TDir : IDirection {
		public IStreamConsumer<ITypedFrame<TDir>> Create(IByteSink<TDir> byteSink) {
			return new FrameSink<TDir>(byteSink);
		}
	}
}
