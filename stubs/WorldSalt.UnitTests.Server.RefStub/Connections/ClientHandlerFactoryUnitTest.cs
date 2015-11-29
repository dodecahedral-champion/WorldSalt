namespace WorldSalt.UnitTests.Server.RefStub.Connections {
	using System;
	using System.Net.Sockets;
	using NUnit.Framework;
	using Rhino.Mocks;
	using WorldSalt.Network;
	using WorldSalt.Network.Direction;
	using WorldSalt.Network.Frames;
	using WorldSalt.Network.Streams;
	using WorldSalt.Server.RefStub;
	using WorldSalt.Server.RefStub.Connections;

	[TestFixture]
	public class ClientHandlerFactoryUnitTest {
		private ClientHandlerFactory target;
		private IFrameFactory<FromServer> frameFactory;
		private IFrameStreamFactory streamFactory;

		[SetUp]
		public void Setup() {
			frameFactory = MockRepository.GenerateMock<IFrameFactory<FromServer>>();
			streamFactory = MockRepository.GenerateMock<IFrameStreamFactory>();

			target = new ClientHandlerFactory(frameFactory, streamFactory);
		}

		[Test]
		public void ShouldCreateStreamWhenCreatingClientHandler() {
			var socket = new TcpClient();
			streamFactory.Expect(x => x.CreateDuplexForServer(socket));

			var clientHandler = target.Create(socket);

			Assert.IsNotNull(clientHandler);
			streamFactory.VerifyAllExpectations();
		}
	}
}
