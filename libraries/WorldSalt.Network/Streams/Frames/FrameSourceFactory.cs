namespace WorldSalt.Network.Streams.Frames {
	using WorldSalt.Network.Direction;
	using WorldSalt.Network.Frames;
	using WorldSalt.Network.Payloads;
	using WorldSalt.Network.Streams.Bytes;

	public class FrameSourceFactory<TDir> : IFrameSourceFactory<TDir> where TDir : IDirection {
		private IPayloadFactory<TDir> payloadFactory;
		private IFrameFactory<TDir> frameFactory;

		public FrameSourceFactory(IPayloadFactory<TDir> payloadFactory, IFrameFactory<TDir> frameFactory) {
			this.payloadFactory = payloadFactory;
			this.frameFactory = frameFactory;
		}

		public IStreamProducer<ITypedFrame<TDir>> Create(IByteSource<TDir> byteSource) {
			var rawFrameSource = new RawFrameSource<TDir>(byteSource, payloadFactory, frameFactory);
			return new FrameSource<TDir>(rawFrameSource, payloadFactory, frameFactory);
		}
	}
}
