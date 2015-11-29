namespace WorldSalt.UnitTests.Network.Payloads.Connection {
	using System;
	using NUnit.Framework;
	using Rhino.Mocks;
	using WorldSalt.Network;
	using WorldSalt.Network.Payloads.Connection;

	[TestFixture]
	public class ConnectPayloadUnitTest {
		private ConnectPayload target;

		[SetUp]
		public void Setup() {
			target = new ConnectPayload();
		}

		[Test]
		public void ShouldConstructWithDefaults() {
			var localTarget = new ConnectPayload();

			Assert.AreEqual("", localTarget.Username);
			Assert.AreEqual(ProtocolVersion.CURRENT, localTarget.PreferredProtocol);
			CollectionAssert.IsEmpty(localTarget.SupportedProtocols);
		}

		[Test]
		public void ShouldSetFromBytes() {
			target.SetBytes(new byte[] {
				0x03, 0x00, 0x00, 0x00,
				0x66, 0x6f, 0x6f,
				42, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00,
				0x03, 0x00, 0x00, 0x00,
				39, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00,
				40, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00,
				41, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00
			});

			Assert.AreEqual("foo", target.Username);
			Assert.AreEqual(42, target.PreferredProtocol);
			CollectionAssert.AreEqual(new[] { 39, 40, 41 }, target.SupportedProtocols);
		}

		[Test]
		public void ShouldGetBytes() {
			var expectedBytes = new byte[] {
				0x03, 0x00, 0x00, 0x00,
				0x66, 0x6f, 0x6f,
				42, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00,
				0x03, 0x00, 0x00, 0x00,
				39, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00,
				40, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00,
				41, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00
			};
			var localTarget = new ConnectPayload("foo", 42, new UInt64[] { 39, 40, 41 });

			var actualBytes = localTarget.GetBytes();

			CollectionAssert.AreEqual(expectedBytes, actualBytes);
		}
	}
}
