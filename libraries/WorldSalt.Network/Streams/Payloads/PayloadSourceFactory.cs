namespace WorldSalt.Network.Streams.Payloads {
	using WorldSalt.Network.Direction;
	using WorldSalt.Network.Frames;
	using WorldSalt.Network.Payloads;
	using WorldSalt.Network.Streams.Bytes;
	using WorldSalt.Network.Streams.Frames;

	public class PayloadSourceFactory<TDir> : IPayloadSourceFactory<TDir> where TDir : IDirection {
		private IFrameSourceFactory<TDir> frameSourceFactory;

		public PayloadSourceFactory(IFrameSourceFactory<TDir> frameSourceFactory) {
			this.frameSourceFactory = frameSourceFactory;
		}

		public IStreamProducer<ITypedPayload<TDir>> Create(IByteSource<TDir> byteSource) {
			var typedFrameSource = frameSourceFactory.Create(byteSource);
			return new PayloadSource<TDir>(typedFrameSource);
		}
	}
}
