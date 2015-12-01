namespace WorldSalt.UnitTests.Network.Streams.Payloads {
    using NUnit.Framework;
    using Rhino.Mocks;
    using WorldSalt.Network.Direction;
    using WorldSalt.Network.Frames;
    using WorldSalt.Network.Payloads;
    using WorldSalt.Network.Streams;
    using WorldSalt.Network.Streams.Bytes;
    using WorldSalt.Network.Streams.Frames;
    using WorldSalt.Network.Streams.Payloads;

    [TestFixture]
	public class PayloadSinkFactoryUnitTest {
		private IFrameSinkFactory<FromServer> frameSinkFactory;
		private IFrameFactory<FromServer> frameFactory;
		private IByteSink<FromServer> byteSink;
		private IStreamConsumer<ITypedFrame<FromServer>> frameSink;
		private ITypedFrame<ITypedPayload<FromServer>, FromServer> frame;
		private ITypedPayload<FromServer> payload;

		private PayloadSinkFactory<FromServer> target;

		[SetUp]
		public void Setup() {
			frameSinkFactory = MockRepository.GenerateMock<IFrameSinkFactory<FromServer>>();
			frameFactory = MockRepository.GenerateMock<IFrameFactory<FromServer>>();
			byteSink = MockRepository.GenerateMock<IByteSink<FromServer>>();
			frameSink = MockRepository.GenerateMock<IStreamConsumer<ITypedFrame<FromServer>>>();
			frame = MockRepository.GenerateMock<ITypedFrame<ITypedPayload<FromServer>, FromServer>>();
			payload = MockRepository.GenerateMock<ITypedPayload<FromServer>>();

			target = new PayloadSinkFactory<FromServer>(frameSinkFactory, frameFactory);
		}

		[Test]
		public void ShouldCreateFrameSinkOnCreate() {
			frameSinkFactory.Expect(x => x.Create(byteSink)).Return(frameSink);

			var sink = target.Create(byteSink);
			sink.Dispose();

			frameSinkFactory.VerifyAllExpectations();
		}

		[Test]
		public void ShouldCallUnderlyingSourceOnTake() {
			frameFactory.Expect(x => x.Create(payload)).Return(frame);
			frame.Stub(x => x.Payload).Return(payload);
			frameSinkFactory.Stub(x => x.Create(byteSink)).Return(frameSink);
			frameSink.Expect(x => x.Put(frame));

			target.Create(byteSink).Put(payload);

			frameSink.VerifyAllExpectations();
			frameFactory.VerifyAllExpectations();
		}
	}
}
