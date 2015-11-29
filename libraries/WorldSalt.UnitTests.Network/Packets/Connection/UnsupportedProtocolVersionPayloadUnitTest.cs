namespace WorldSalt.UnitTests.Network.Packets.Connection {
	using System;
	using NUnit.Framework;
	using Rhino.Mocks;
	using WorldSalt.Network;
	using WorldSalt.Network.Packets.Connection;

	[TestFixture]
	public class UnsupportedProtocolVersionPayloadUnitTest {
		private UnsupportedProtocolVersionPayload target;

		[SetUp]
		public void Setup() {
			target = new UnsupportedProtocolVersionPayload();
		}

		[Test]
		public void ShouldConstructWithDefaults() {
			var localTarget = new UnsupportedProtocolVersionPayload();

			Assert.AreEqual(ProtocolVersion.CURRENT, localTarget.PreferredProtocol);
			CollectionAssert.IsEmpty(localTarget.SupportedProtocols);
		}

		[Test]
		public void ShouldSetFromBytes() {
			target.SetBytes(new byte[] {
				42, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00,
				0x03, 0x00, 0x00, 0x00,
				39, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00,
				40, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00,
				41, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00
			});

			Assert.AreEqual(42, target.PreferredProtocol);
			CollectionAssert.AreEqual(new[] { 39, 40, 41 }, target.SupportedProtocols);
		}

		[Test]
		public void ShouldGetBytes() {
			var expectedBytes = new byte[] {
				42, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00,
				0x03, 0x00, 0x00, 0x00,
				39, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00,
				40, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00,
				41, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00
			};
			var localTarget = new UnsupportedProtocolVersionPayload(42, new UInt64[] { 39, 40, 41 });

			var actualBytes = localTarget.GetBytes();

			CollectionAssert.AreEqual(expectedBytes, actualBytes);
		}
	}
}
