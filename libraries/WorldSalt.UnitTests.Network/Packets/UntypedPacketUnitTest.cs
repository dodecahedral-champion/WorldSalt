namespace WorldSalt.UnitTests.Network.Packets {
	using System;
	using System.Collections.Generic;
	using NUnit.Framework;
	using Rhino.Mocks;
	using WorldSalt.Network.Direction;
	using WorldSalt.Network.Packets;
	using WorldSalt.Network.Packets.Connection;

	[TestFixture]
	public class UntypedPacketUnitTest {
		[Test]
		public void ShouldConvertToConnectPacket() {
			var target = new UntypedPacket<FromClient>();
			target.Payload.SetBytes(new byte[] {
				0x03, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00,
				0x66, 0x6f, 0x6f,
				  42, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00,
				0x03, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00,
				  39, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00,
				  40, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00,
				  41, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00
			});

			ITypedPacket<ConnectPayload, FromClient> converted = null;
			Assert.DoesNotThrow(() => converted = target.ConvertToTyped<ConnectPayload>());

			Assert.IsNotNull(converted);
			Assert.AreEqual("foo", converted.Payload.Username);
			Assert.AreEqual(42, converted.Payload.PreferredProtocol);
			CollectionAssert.AreEqual(new List<ulong> { 39, 40, 41 }, converted.Payload.SupportedProtocols);
		}
	}
}

