namespace WorldSalt.UnitTests.Network.Payloads.Connection {
	using NUnit.Framework;
	using WorldSalt.Network;
	using WorldSalt.Network.Payloads.Connection;

	[TestFixture]
	public class ConnectedPayloadUnitTest {
		private ConnectedPayload target;

		[SetUp]
		public void Setup() {
			target = new ConnectedPayload();
		}

		[Test]
		public void ShouldConstructWithDefaults() {
			var localTarget = new ConnectedPayload();

			Assert.AreEqual(ProtocolVersion.CURRENT, localTarget.Protocol);
		}

		[Test]
		public void ShouldSetFromBytes() {
			target.SetBytes(new byte[] {
				42, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00,
			});

			Assert.AreEqual(42, target.Protocol);
		}

		[Test]
		public void ShouldGetBytes() {
			var expectedBytes = new byte[] {
				42, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00,
			};
			var localTarget = new ConnectedPayload(42);

			var actualBytes = localTarget.GetBytes();

			CollectionAssert.AreEqual(expectedBytes, actualBytes);
		}
	}
}
