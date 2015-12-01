namespace WorldSalt.UnitTests.Network.Payloads.Connection {
	using NUnit.Framework;
	using WorldSalt.Network.Payloads.Connection;

	[TestFixture]
	public class KickedPayloadUnitTest {
		private KickedPayload target;

		[SetUp]
		public void Setup() {
			target = new KickedPayload();
		}

		[Test]
		public void ShouldConstructWithDefaults() {
			var localTarget = new KickedPayload();

			Assert.AreEqual("", localTarget.Message);
		}

		[Test]
		public void ShouldSetFromBytes() {
			target.SetBytes(new byte[] {
				0x03, 0x00, 0x00, 0x00,
				0x66, 0x6f, 0x6f,
			});

			Assert.AreEqual("foo", target.Message);
		}

		[Test]
		public void ShouldGetBytes() {
			var expectedBytes = new byte[] {
				0x03, 0x00, 0x00, 0x00,
				0x66, 0x6f, 0x6f,
			};
			var localTarget = new KickedPayload("foo");

			var actualBytes = localTarget.GetBytes();

			CollectionAssert.AreEqual(expectedBytes, actualBytes);
		}
	}
}
