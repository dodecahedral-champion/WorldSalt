namespace WorldSalt.Network.Streams.Payloads {
	using WorldSalt.Network.Direction;
    using WorldSalt.Network.Frames;
    using WorldSalt.Network.Payloads;
	using WorldSalt.Network.Streams.Bytes;
    using WorldSalt.Network.Streams.Frames;

    public class PayloadSinkFactory<TDir> : IPayloadSinkFactory<TDir> where TDir : IDirection {
		private readonly IFrameSinkFactory<TDir> frameSinkFactory;
		private readonly IFrameFactory<TDir> frameFactory;

		public PayloadSinkFactory(IFrameSinkFactory<TDir> frameSinkFactory, IFrameFactory<TDir> frameFactory) {
			this.frameSinkFactory = frameSinkFactory;
			this.frameFactory = frameFactory;
		}

		public IStreamConsumer<ITypedPayload<TDir>> Create(IByteSink<TDir> byteSink) {
			var frameSink = frameSinkFactory.Create(byteSink);
			return new PayloadSink<TDir>(frameSink, frameFactory);
		}
	}
}
