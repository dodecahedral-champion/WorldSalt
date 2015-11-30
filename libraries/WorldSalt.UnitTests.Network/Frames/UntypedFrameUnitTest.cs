namespace WorldSalt.UnitTests.Network.Frames {
	using System;
	using NUnit.Framework;
	using Rhino.Mocks;
	using WorldSalt.Network.Direction;
	using WorldSalt.Network.Frames;
	using WorldSalt.Network.Payloads;

	[TestFixture]
	public class UntypedFrameUnitTest {
		[Test]
		public void ShouldConstructWithDefaults() {
			var target = new UntypedFrame<FromClient>();

			Assert.AreEqual(0x00, target.Type);
			Assert.AreEqual(0x00, target.Subtype);
			Assert.NotNull(target.Payload);
			Assert.AreEqual(0, target.Payload.Length);
		}

		[Test]
		public void ShouldConstructWithArguments() {
			var expectedType = (byte)0x12;
			var expectedSubtype = (byte)0x34;
			var expectedPayload = MockRepository.GenerateMock<IRawPayload<FromClient>>();
			var target = new UntypedFrame<FromClient>(expectedType, expectedSubtype, expectedPayload);

			Assert.AreEqual(expectedType, target.Type);
			Assert.AreEqual(expectedSubtype, target.Subtype);
			Assert.AreSame(expectedPayload, target.Payload);
		}
	}
}
