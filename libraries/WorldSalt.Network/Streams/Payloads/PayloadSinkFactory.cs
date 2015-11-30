using WorldSalt.Network.Streams.Frames;
using WorldSalt.Network.Frames;

namespace WorldSalt.Network.Streams.Payloads {
	using WorldSalt.Network.Direction;
	using WorldSalt.Network.Payloads;
	using WorldSalt.Network.Streams.Bytes;

	public class PayloadSinkFactory<TDir> : IPayloadSinkFactory<TDir> where TDir : IDirection {
		private IFrameSinkFactory<TDir> frameSinkFactory;
		private IFrameFactory<TDir> frameFactory;

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
