namespace WorldSalt.UnitTests.Network.Frames {
	using System;
	using NUnit.Framework;
	using Rhino.Mocks;
	using WorldSalt.Network.Direction;
	using WorldSalt.Network.Frames;
	using WorldSalt.Network.Payloads;
	using WorldSalt.Network.Payloads.Connection;

	[TestFixture]
	public class FrameFactoryUnitTest {
		private IPayloadFactory<FromClient> payloadFactoryC;
		private FrameFactory<FromClient> targetC;

		[SetUp]
		public void Setup() {
			payloadFactoryC = MockRepository.GenerateMock<IPayloadFactory<FromClient>>();
			targetC = new FrameFactory<FromClient>(payloadFactoryC);
		}

		[Test]
		public void ShouldCallPayloadFactoryWhenConvertingUntypedFrame() {
			var untypedFrame = MockRepository.GenerateMock<IUntypedFrame<FromClient>>();
			var untypedPayload = MockRepository.GenerateMock<IRawPayload<FromClient>>();
			untypedFrame.Stub(x => x.Payload).Return(untypedPayload);
			var typedPayload = new DisconnectPayload();
			payloadFactoryC.Expect(x => x.ConvertPayload<DisconnectPayload>(untypedPayload)).Return(typedPayload);

			var typedFrame = targetC.ConvertToTyped<DisconnectPayload>(untypedFrame);

			Assert.NotNull(typedFrame);
			payloadFactoryC.VerifyAllExpectations();
			Assert.AreSame(typedPayload, typedFrame.Payload);
		}
	}
}
