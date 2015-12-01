namespace WorldSalt.UnitTests.Network.Payloads {
	using NUnit.Framework;
	using Rhino.Mocks;
	using WorldSalt.Network.Direction;
	using WorldSalt.Network.Payloads;
	using WorldSalt.Network.Payloads.Connection;

	[TestFixture]
	public class PayloadFactoryUnitTest {
		private IPayloadFactory<FromClient> targetC;
		private IPayloadTypedCreator<FromClient> payloadTypedCreator;

		[SetUp]
		public void Setup() {
			payloadTypedCreator = MockRepository.GenerateMock<IPayloadTypedCreator<FromClient>>();
			targetC = new PayloadFactory<FromClient>(payloadTypedCreator);
		}

		[Test]
		public void ShouldConvertUntypedPayloadToConnectPayload() {
			var rawPayload = MockRepository.GenerateMock<IRawPayload<FromClient>>();
			rawPayload.Expect(x => x.GetBytes()).Return(new byte[] {
				0x03, 0x00, 0x00, 0x00,
				0x66, 0x6f, 0x6f,
				42, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00,
				0x03, 0x00, 0x00, 0x00,
				39, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00,
				40, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00,
				41, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00
			});

			var convertedPayload = targetC.ConvertPayload<ConnectPayload>(rawPayload);

			Assert.AreEqual("foo", convertedPayload.Username);
			Assert.AreEqual(42, convertedPayload.PreferredProtocol);
			CollectionAssert.AreEqual(new[] { 39, 40, 41 }, convertedPayload.SupportedProtocols);
			rawPayload.VerifyAllExpectations();
		}

		[Test]
		public void ShouldCallCreatorToDeterminePayloadTypeFromCodes() {
			var rawBytes = new byte[] { 0x12, 0x34, 0x56 };
			var rawPayload = MockRepository.GenerateMock<IRawPayload<FromClient>>();
			var typedPayload = MockRepository.GenerateMock<ITypedPayload<FromClient>>();
			rawPayload.Expect(x => x.GetBytes()).Return(rawBytes);
			typedPayload.Expect(x => x.SetBytes(rawBytes));
			payloadTypedCreator.Expect(x => x.Create(0x00, 0x03)).Return(typedPayload);

			var convertedPayload = targetC.ConvertPayload(rawPayload, 0x00, 0x03);

			Assert.AreSame(typedPayload, convertedPayload);
			payloadTypedCreator.VerifyAllExpectations();
			rawPayload.VerifyAllExpectations();
			typedPayload.VerifyAllExpectations();
		}
	}
}
