namespace WorldSalt.UnitTests.Server.RefStub.Connections {
	using System;
	using System.Net.Sockets;
	using NUnit.Framework;
	using Rhino.Mocks;
	using WorldSalt.Network;
	using WorldSalt.Network.Direction;
	using WorldSalt.Network.Packets;
	using WorldSalt.Server.RefStub;
	using WorldSalt.Server.RefStub.Connections;

	[TestFixture]
	public class ClientHandlerFactoryUnitTest {
		private ClientHandlerFactory target;
		private IPacketFactory<FromServer> packetFactory;
		private IPacketStreamFactory packetStreamFactory;

		[SetUp]
		public void Setup() {
			packetFactory = MockRepository.GenerateMock<IPacketFactory<FromServer>>();
			packetStreamFactory = MockRepository.GenerateMock<IPacketStreamFactory>();

			target = new ClientHandlerFactory(packetFactory, packetStreamFactory);
		}

		[Test]
		public void ShouldCreatePacketStreamWhenCreatingClientHandler() {
			var socket = new TcpClient();
			packetStreamFactory.Expect(x => x.CreateDuplexForServer(socket));

			var clientHandler = target.Create(socket);

			Assert.IsNotNull(clientHandler);
			packetStreamFactory.VerifyAllExpectations();
		}
	}
}
