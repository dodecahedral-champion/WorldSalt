namespace WorldSalt.UnitTests.Network.Payloads.Ping {
	using System;
	using NUnit.Framework;
	using WorldSalt.Network.Payloads.Ping;

	[TestFixture]
	public class ClientPingResponsePayloadUnitTest {
		private ClientPingResponsePayload target;

		[SetUp]
		public void Setup() {
			target = new ClientPingResponsePayload();
		}

		[Test]
		public void ShouldConstructWithDefaults() {
			var localTarget = new ClientPingResponsePayload();

			Assert.AreEqual(Guid.Empty, localTarget.RequestId);
		}

		[Test]
		public void ShouldSetFromBytes() {
			var expectedGuid = new Guid("93844966-4241-42ad-90a0-83bcfa4a0e62");
			target.SetBytes(new byte[] {
				0x66, 0x49, 0x84, 0x93,
				0x41, 0x42,
				0xad, 0x42,
				0x90, 0xa0,
				0x83, 0xbc, 0xfa, 0x4a, 0xe, 0x62
			});

			Assert.AreEqual(expectedGuid, target.RequestId);
		}

		[Test]
		public void ShouldGetBytes() {
			var expectedBytes = new byte[] {
				0x66, 0x49, 0x84, 0x93,
				0x41, 0x42,
				0xad, 0x42,
				0x90, 0xa0,
				0x83, 0xbc, 0xfa, 0x4a, 0xe, 0x62
			};
			var guid = new Guid("93844966-4241-42ad-90a0-83bcfa4a0e62");
			var localTarget = new ClientPingResponsePayload(guid);

			var actualBytes = localTarget.GetBytes();

			CollectionAssert.AreEqual(expectedBytes, actualBytes);
		}
	}
}
