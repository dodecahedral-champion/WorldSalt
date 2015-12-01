namespace WorldSalt.UnitTests.Network.Frames {
    using NUnit.Framework;
    using Rhino.Mocks;
    using WorldSalt.Network.Direction;
    using WorldSalt.Network.Frames;
    using WorldSalt.Network.Payloads;

    [TestFixture]
	public class FrameUnitTest {
		private FrameFactory<FromServer> factory;
		private IPayloadFactory<FromServer> payloadFactory;

		[SetUp]
		public void Setup() {
			payloadFactory = MockRepository.GenerateMock<IPayloadFactory<FromServer>>();
			factory = new FrameFactory<FromServer>(payloadFactory);
		}

		[Test]
		public void ShouldSerialiseBasedOnPayload() {
			var expectedBytes = new byte[] {
				0x05, 0x00, 0x00, 0x00,
				0x12,
				0x34,
				0x10, 0x20, 0x30, 0x40, 0xff
			};
			var payload = MockRepository.GenerateMock<ITypedPayload<FromServer>>();
			payload.Expect(x => x.Length).Return(5);
			payload.Expect(x => x.Type).Return(0x12);
			payload.Expect(x => x.Subtype).Return(0x34);
			payload.Expect(x => x.GetBytes()).Return(new byte[] { 0x10, 0x20, 0x30, 0x40, 0xff });
			var target = factory.Create(payload);

			var actualBytes = target.GetBytes();

			CollectionAssert.AreEqual(expectedBytes, actualBytes);
		}
	}
}
