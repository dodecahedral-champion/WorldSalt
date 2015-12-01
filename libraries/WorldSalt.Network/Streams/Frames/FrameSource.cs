namespace WorldSalt.Network.Streams.Frames {
	using WorldSalt.Network.Direction;
	using WorldSalt.Network.Frames;
	using WorldSalt.Network.Payloads;

	public class FrameSource<TDir> : IStreamProducer<ITypedFrame<TDir>> where TDir : IDirection {
		private readonly IRawFrameSource<TDir> underlying;
		private readonly IPayloadFactory<TDir> payloadFactory;
		private readonly IFrameFactory<TDir> frameFactory;

		public FrameSource(IRawFrameSource<TDir> underlying, IPayloadFactory<TDir> payloadFactory, IFrameFactory<TDir> frameFactory) {
			this.underlying = underlying;
			this.payloadFactory = payloadFactory;
			this.frameFactory = frameFactory;
		}

		public void Dispose() {
			underlying.Dispose();
		}

		public void Close() {
			underlying.Close();
		}

		public ITypedFrame<TDir> Take() {
			var rawFrame = underlying.Take();
			var typedPayload = payloadFactory.ConvertPayload(rawFrame.Payload, rawFrame.Type, rawFrame.Subtype);
			return frameFactory.Create(typedPayload);
		}
	}
}
