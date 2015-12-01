namespace WorldSalt.UnitTests.Network.Streams.Payloads {
	using NUnit.Framework;
	using Rhino.Mocks;
	using WorldSalt.Network.Direction;
	using WorldSalt.Network.Frames;
	using WorldSalt.Network.Payloads;
	using WorldSalt.Network.Streams;
	using WorldSalt.Network.Streams.Payloads;

	[TestFixture]
	public class PayloadSourceUnitTest {
		private PayloadSource<FromClient> target;
		private IStreamProducer<ITypedFrame<FromClient>> frameStream;

		[SetUp]
		public void Setup() {
			frameStream = MockRepository.GenerateMock<IStreamProducer<ITypedFrame<FromClient>>>();

			target = new PayloadSource<FromClient>(frameStream);
		}

		[Test]
		public void ShouldCloseUnderlying() {
			frameStream.Expect(x => x.Close());

			target.Close();

			frameStream.VerifyAllExpectations();
		}

		[Test]
		public void ShouldDisposeUnderlying() {
			frameStream.Expect(x => x.Dispose());

			target.Dispose();

			frameStream.VerifyAllExpectations();
		}

		[Test]
		public void ShouldConvertFromUnderlying() {
			var frame = MockRepository.GenerateMock<ITypedFrame<FromClient>>();
			var payload = MockRepository.GenerateMock<ITypedPayload<FromClient>>();
			frame.Expect(x => x.Payload).Return(payload);
			frameStream.Expect(x => x.Take()).Return(frame);

			var actualPayload = target.Take();

			Assert.AreSame(payload, actualPayload);
			frameStream.VerifyAllExpectations();
			frame.VerifyAllExpectations();
		}
	}
}
