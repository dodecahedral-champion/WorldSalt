namespace WorldSalt.Network.Streams.Frames {
	using System;
	using WorldSalt.Network.Direction;
	using WorldSalt.Network.Frames;
	using WorldSalt.Network.Payloads;
	using WorldSalt.Network.Streams.Bytes;
	using WorldSalt.Network.SerialisationExtensions;

	public class FrameSource<TDir> : IStreamProducer<ITypedFrame<TDir>> where TDir : IDirection {
		private IRawFrameSource<TDir> underlying;
		private IPayloadFactory<TDir> payloadFactory;
		private IFrameFactory<TDir> frameFactory;

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
