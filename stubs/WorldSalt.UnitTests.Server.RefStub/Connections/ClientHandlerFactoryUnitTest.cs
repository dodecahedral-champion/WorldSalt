namespace WorldSalt.UnitTests.Server.RefStub.Connections {
	using System;
	using System.Net.Sockets;
	using NUnit.Framework;
	using Rhino.Mocks;
	using WorldSalt.Network;
	using WorldSalt.Server.RefStub;
	using WorldSalt.Server.RefStub.Connections;

	[TestFixture]
	public class ClientHandlerFactoryUnitTest {
		private ClientHandlerFactory target;
		private IPacketStreamFactory packetStreamFactory;

		[SetUp]
		public void Setup() {
			packetStreamFactory = MockRepository.GenerateMock<IPacketStreamFactory>();

			target = new ClientHandlerFactory(packetStreamFactory);
		}

		[Test]
		public void ShouldCreatePacketStreamWhenCreatingClientHandler() {
			var socket = new TcpClient();
			packetStreamFactory.Expect(x => x.CreateDuplex(socket));

			var clientHandler = target.Create(socket);

			Assert.IsNotNull(clientHandler);
			packetStreamFactory.VerifyAllExpectations();
		}
	}
}
