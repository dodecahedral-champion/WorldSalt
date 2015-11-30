namespace WorldSalt.UnitTests.Server.RefStub.Connections {
	using System;
	using System.Net.Sockets;
	using NUnit.Framework;
	using Rhino.Mocks;
	using WorldSalt.Server.RefStub;
	using WorldSalt.Server.RefStub.Connections;

	[TestFixture]
	public class ConnectionFoyerUnitTest {
		private ConnectionFoyer target;
		const int LISTEN_PORT = 1117;
		private IClientHandlerFactory clientFactory;
		private IClientHandler clientHandler;

		[SetUp]
		public void Setup() {
			clientFactory = MockRepository.GenerateMock<IClientHandlerFactory>();
			clientHandler = MockRepository.GenerateMock<IClientHandler>();
			clientFactory.Stub(x => x.Create(null, null)).IgnoreArguments().Return(clientHandler);

			target = new ConnectionFoyer(clientFactory, LISTEN_PORT);
		}

		[TearDown]
		public void Teardown() {
			target.Dispose();
		}

		[Test]
		public void ShouldCallClientHandlerFactoryWhenClientConnects() {
			var remote = new TcpClient("localhost", LISTEN_PORT);
			var actualClientHandler = target.AcceptOne();
			remote.Close();

			Assert.AreSame(clientHandler, actualClientHandler);
		}
	}
}
