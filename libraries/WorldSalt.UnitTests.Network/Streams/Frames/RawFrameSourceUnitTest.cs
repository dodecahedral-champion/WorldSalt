namespace WorldSalt.UnitTests.Network.Streams.Frames {
	using NUnit.Framework;
	using Rhino.Mocks;
	using WorldSalt.Network.Direction;
	using WorldSalt.Network.Frames;
	using WorldSalt.Network.Payloads;
	using WorldSalt.Network.Streams.Bytes;
	using WorldSalt.Network.Streams.Frames;

	[TestFixture]
	public class RawFrameSourceUnitTest {
		private RawFrameSource<FromClient> target;
		private IByteSource<FromClient> byteStream;
		private IPayloadFactory<FromClient> payloadFactory;
		private IFrameFactory<FromClient> frameFactory;

		[SetUp]
		public void Setup() {
			byteStream = MockRepository.GenerateMock<IByteSource<FromClient>>();
			payloadFactory = MockRepository.GenerateMock<IPayloadFactory<FromClient>>();
			frameFactory = MockRepository.GenerateMock<IFrameFactory<FromClient>>();

			target = new RawFrameSource<FromClient>(byteStream, payloadFactory, frameFactory);
		}

		[Test]
		public void ShouldCloseUnderlying() {
			byteStream.Expect(x => x.Close());

			target.Close();

			byteStream.VerifyAllExpectations();
		}

		[Test]
		public void ShouldDisposeUnderlying() {
			byteStream.Expect(x => x.Dispose());

			target.Dispose();

			byteStream.VerifyAllExpectations();
		}

		[Test]
		public void ShouldTakeBytesFromUnderlying() {
			var expectedType = (byte)0x00;
			var expectedSubtype = (byte)0x04;
			var payloadBytes = new byte[] { 0x03, 0x00, 0x00, 0x00, 0x66, 0x6f, 0x6f };
			byteStream.Expect(x => x.ReadSequence(4)).Return(new byte[] { 0x07, 0x00, 0x00, 0x00 }).Repeat.Once();
			byteStream.Expect(x => x.Read()).Return(expectedType).Repeat.Once();
			byteStream.Expect(x => x.Read()).Return(expectedSubtype).Repeat.Once();
			byteStream.Expect(x => x.ReadSequence(7)).Return(payloadBytes).Repeat.Once();
			var payload = MockRepository.GenerateMock<IRawPayload<FromClient>>();
			var rawFrame = MockRepository.GenerateMock<IUntypedFrame<FromClient>>();
			payloadFactory.Expect(x => x.CreateUntypedPayload(payloadBytes)).Return(payload);
			frameFactory.Expect(x => x.CreateUntyped(expectedType, expectedSubtype, payload)).Return(rawFrame);

			var actualFrame = target.Take();

			Assert.AreEqual(rawFrame, actualFrame);
			byteStream.VerifyAllExpectations();
			actualFrame.VerifyAllExpectations();
			payloadFactory.VerifyAllExpectations();
			frameFactory.VerifyAllExpectations();
		}
	}
}
