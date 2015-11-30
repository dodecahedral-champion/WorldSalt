namespace WorldSalt.UnitTests.Network.Payloads {
	using System;
	using NUnit.Framework;
	using WorldSalt.Network;
	using WorldSalt.Network.Direction;
	using WorldSalt.Network.Payloads;
	using WorldSalt.Network.Payloads.Connection;
	using WorldSalt.Network.Payloads.Ping;

	[TestFixture]
	public class PayloadTypeVerification {
		[Test]
		public void ShouldHaveCorrectTypeAndSubtypesForConnectionPayloads() {
			VerifyTypes<FromClient>(0x00, 0x00).For<ConnectPayload>();
			VerifyTypes<FromServer>(0x00, 0x01).For<ConnectedPayload>();
			VerifyTypes<FromServer>(0x00, 0x02).For<UnsupportedProtocolVersionPayload>();
			VerifyTypes<FromClient>(0x00, 0x03).For<DisconnectPayload>();
			VerifyTypes<FromServer>(0x00, 0x04).For<KickedPayload>();
		}

		[Test]
		public void ShouldHaveCorrectTypeAndSubtypesForPingPayloads() {
			VerifyTypes<FromClient>(0x01, 0x00).For<ClientPingPayload>();
			VerifyTypes<FromServer>(0x01, 0x01).For<ClientPingResponsePayload>();
			VerifyTypes<FromServer>(0x01, 0x02).For<ServerPingPayload>();
			VerifyTypes<FromClient>(0x01, 0x03).For<ServerPingResponsePayload>();
		}

		private Verifier<TDirection> VerifyTypes<TDirection>(Byte expectedType, Byte expectedSubtype) where TDirection : IDirection {
			return new Verifier<TDirection> { ExpectedType = expectedType, ExpectedSubType = expectedSubtype };
		}

		private void VerifyPayloadTypes<TPayload, TDirection>(Byte expectedType, Byte expectedSubtype) where TPayload : ITypedPayload<TDirection>, new() where TDirection : IDirection {
			var payload = new TPayload();
			Assert.AreEqual(expectedType, payload.Type);
			Assert.AreEqual(expectedSubtype, payload.Subtype);
		}

		private class Verifier<TDirection> where TDirection : IDirection {
			public Byte ExpectedType { get; set; }
			public Byte ExpectedSubType { get; set; }

			public void For<TConcretePayload>() where TConcretePayload : ITypedPayload<TDirection>, new() {
				var payload = new TConcretePayload();
				Assert.AreEqual(ExpectedType, payload.Type);
				Assert.AreEqual(ExpectedSubType, payload.Subtype);
			}
		}
	}
}

