namespace WorldSalt.UnitTests.Network.Streams {
	using System;
	using NUnit.Framework;
	using Rhino.Mocks;
	using WorldSalt.Network.Direction;
	using WorldSalt.Network.Frames;
	using WorldSalt.Network.Payloads;
	using WorldSalt.Network.Streams;

	[TestFixture]
	public class FrameSourceUnitTest {
		private FrameSource<FromClient> target;
		private IRawFrameSource<FromClient> rawStream;
		private IPayloadFactory<FromClient> payloadFactory;
		private IFrameFactory<FromClient> frameFactory;

		[SetUp]
		public void Setup() {
			rawStream = MockRepository.GenerateMock<IRawFrameSource<FromClient>>();
			payloadFactory = MockRepository.GenerateMock<IPayloadFactory<FromClient>>();
			frameFactory = MockRepository.GenerateMock<IFrameFactory<FromClient>>();

			target = new FrameSource<FromClient>(rawStream, payloadFactory, frameFactory);
		}

		[Test]
		public void ShouldCloseUnderlying() {
			rawStream.Expect(x => x.Close());

			target.Close();

			rawStream.VerifyAllExpectations();
		}

		[Test]
		public void ShouldDisposeUnderlying() {
			rawStream.Expect(x => x.Dispose());

			target.Dispose();

			rawStream.VerifyAllExpectations();
		}

		[Test]
		public void ShouldConvertFromUnderlying() {
			var expectedType = (byte)0x00;
			var expectedSubtype = (byte)0x04;
			var rawFrame = MockRepository.GenerateMock<IUntypedFrame<FromClient>>();
			var payload = MockRepository.GenerateMock<IRawPayload<FromClient>>();
			rawFrame.Expect(x => x.Payload).Return(payload);
			rawFrame.Expect(x => x.Type).Return(expectedType);
			rawFrame.Expect(x => x.Subtype).Return(expectedSubtype);
			rawStream.Expect(x => x.Take()).Return(rawFrame);
			var typedPayload = MockRepository.GenerateMock<ITypedPayload<FromClient>>();
			var typedFrame = MockRepository.GenerateMock<ITypedFrame<ITypedPayload<FromClient>, FromClient>>();
			payloadFactory.Expect(x => x.ConvertPayload(payload, expectedType, expectedSubtype)).Return(typedPayload);
			frameFactory.Expect(x => x.Create(typedPayload)).Return(typedFrame);

			var actualFrame = target.Take();

			Assert.AreSame(typedFrame, actualFrame);
			rawStream.VerifyAllExpectations();
			rawFrame.VerifyAllExpectations();;
			payloadFactory.VerifyAllExpectations();
			frameFactory.VerifyAllExpectations();
		}
	}
}
