namespace WorldSalt.UnitTests.Network {
	using System;
	using NUnit.Framework;
	using Rhino.Mocks;
	using WorldSalt.Network.Direction;
	using WorldSalt.Network.Packets;
	using WorldSalt.Network.Packets.Connection;

	[TestFixture]
	public class PacketFactoryUnitTest {
		private IPayloadFactory<FromClient> payloadFactoryC;
		private PacketFactory<FromClient> targetC;

		[SetUp]
		public void Setup() {
			payloadFactoryC = MockRepository.GenerateMock<IPayloadFactory<FromClient>>();
			targetC = new PacketFactory<FromClient>(payloadFactoryC);
		}

		[Test]
		public void ShouldCallPayloadFactoryWhenConvertingUntypedPacket() {
			var untypedPacket = MockRepository.GenerateMock<IUntypedPacket<FromClient>>();
			var untypedPayload = MockRepository.GenerateMock<IRawPayload<FromClient>>();
			untypedPacket.Stub(x => x.Payload).Return(untypedPayload);
			var typedPayload = new DisconnectPayload();
			payloadFactoryC.Expect(x => x.ConvertPayload<DisconnectPayload>(untypedPayload)).Return(typedPayload);

			var typedPacket = targetC.ConvertToTyped<DisconnectPayload>(untypedPacket);

			Assert.NotNull(typedPacket);
			payloadFactoryC.VerifyAllExpectations();
			Assert.AreSame(typedPayload, typedPacket.Payload);
		}
	}
}
