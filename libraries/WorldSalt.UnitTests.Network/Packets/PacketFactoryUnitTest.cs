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
		private IPacketFactory<FromClient> targetC;

		[SetUp]
		public void Setup() {
			payloadFactoryC = MockRepository.GenerateMock<IPayloadFactory<FromClient>>();
			targetC = new PacketFactory<FromClient>(payloadFactoryC);
		}
	}
}
