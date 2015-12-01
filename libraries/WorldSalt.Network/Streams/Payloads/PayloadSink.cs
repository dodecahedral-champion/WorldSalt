namespace WorldSalt.Network.Streams.Payloads {
	using WorldSalt.Network.Direction;
	using WorldSalt.Network.Frames;
	using WorldSalt.Network.Payloads;

	public class PayloadSink<TDir> : IStreamConsumer<ITypedPayload<TDir>> where TDir : IDirection {
		private readonly IStreamConsumer<ITypedFrame<TDir>> underlying;
		private readonly IFrameFactory<TDir> frameFactory;

		public PayloadSink(IStreamConsumer<ITypedFrame<TDir>> underlying, IFrameFactory<TDir> frameFactory) {
			this.underlying = underlying;
			this.frameFactory = frameFactory;
		}

		public void Dispose() {
			underlying.Dispose();
		}

		public void Close() {
			underlying.Close();
		}

		public void Put(ITypedPayload<TDir> value) {
			var frame = frameFactory.Create(value);
			underlying.Put(frame);
		}
	}
}
