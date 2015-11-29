namespace WorldSalt.UnitTests.Network.Payloads.Connection {
	using System;
	using NUnit.Framework;
	using Rhino.Mocks;
	using WorldSalt.Network;
	using WorldSalt.Network.Payloads.Connection;

	[TestFixture]
	public class DisconnectPayloadUnitTest {
		private DisconnectPayload target;

		[SetUp]
		public void Setup() {
			target = new DisconnectPayload();
		}

		[Test]
		public void ShouldSetFromBytes() {
			target.SetBytes(new byte[] {});
		}

		[Test]
		public void ShouldGetBytes() {
			var expectedBytes = new byte[] {};
			var localTarget = new DisconnectPayload();

			var actualBytes = localTarget.GetBytes();

			CollectionAssert.AreEqual(expectedBytes, actualBytes);
		}
	}
}
