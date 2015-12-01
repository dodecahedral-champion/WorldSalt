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
	public class PayloadSourceFactoryUnitTest {
		private IFrameSourceFactory<FromServer> frameSourceFactory;
		private IByteSource<FromServer> byteSource;
		private IStreamProducer<ITypedFrame<FromServer>> frameSource;
		private ITypedFrame<FromServer> frame;
		private ITypedPayload<FromServer> payload;

		private PayloadSourceFactory<FromServer> target;

		[SetUp]
		public void Setup() {
			frameSourceFactory = MockRepository.GenerateMock<IFrameSourceFactory<FromServer>>();
			byteSource = MockRepository.GenerateMock<IByteSource<FromServer>>();
			frameSource = MockRepository.GenerateMock<IStreamProducer<ITypedFrame<FromServer>>>();
			frame = MockRepository.GenerateMock<ITypedFrame<FromServer>>();
			payload = MockRepository.GenerateMock<ITypedPayload<FromServer>>();

			target = new PayloadSourceFactory<FromServer>(frameSourceFactory);
		}

		[Test]
		public void ShouldCreateFrameSourceOnCreate() {
			frameSourceFactory.Expect(x => x.Create(byteSource)).Return(frameSource);

			var source = target.Create(byteSource);
			source.Dispose();

			frameSourceFactory.VerifyAllExpectations();
		}

		[Test]
		public void ShouldCallUnderlyingSourceOnTake() {
			frameSourceFactory.Stub(x => x.Create(byteSource)).Return(frameSource);
			frameSource.Expect(x => x.Take()).Return(frame);
			frame.Expect(x => x.Payload).Return(payload);

			var actualPayload = target.Create(byteSource).Take();

			Assert.AreSame(payload, actualPayload);
			frameSource.VerifyAllExpectations();
		}
	}
}
