namespace WorldSalt.UnitTests.Server.RefStub {
	using System;
	using System.Net;
	using System.Net.Sockets;
	using System.Threading;
	using NUnit.Framework;
	using Rhino.Mocks;
	using WorldSalt.Network;
	using WorldSalt.Server.RefStub;
	using WorldSalt.Server.RefStub.Connections;

	[TestFixture]
	public class ServerProcessUnitTest {
		const int VERY_LONG_TIMEOUT = 1000;
		private IConnectionFoyer foyer;
		private IClientHandler clientHandler;
		private IClientHandler extraClientHandler;
		private ServerProcess target;

		[SetUp]
		public void Setup() {
			foyer = MockRepository.GenerateMock<IConnectionFoyer>();
			clientHandler = MockRepository.GenerateMock<IClientHandler>();
			extraClientHandler = MockRepository.GenerateMock<IClientHandler>();

			target = new ServerProcess(foyer);
		}

		[Test]
		public void ShouldAcceptInFoyer() {
			foyer.Stub(x => x.AcceptOne()).Return(clientHandler).Repeat.Once();

			target.AcceptOne();

			foyer.VerifyAllExpectations();
		}

		[Test]
		public void ShouldRunClientHandler() {
			foyer.Stub(x => x.AcceptOne()).Return(clientHandler);
			var runSignal = new ManualResetEvent(false);
			clientHandler.Expect(x => x.Run()).WhenCalled(m => runSignal.Set());

			target.AcceptOne();
			runSignal.WaitOne(VERY_LONG_TIMEOUT);

			clientHandler.VerifyAllExpectations();
		}

		[Test]
		public void ShouldNotBlockWaitingForClientHandlerToComplete() {
			foyer.Stub(x => x.AcceptOne()).Return(clientHandler).Repeat.Once();
			foyer.Stub(x => x.AcceptOne()).Return(extraClientHandler).Repeat.Once();
			var client1BlockSignal = new ManualResetEvent(false);
			var client1CompleteSignal = new ManualResetEvent(false);
			var client2CompleteSignal = new ManualResetEvent(false);
			var client1Done = false;
			var client2Done = false;
			clientHandler.Expect(x => x.Run()).WhenCalled(m => {
				client1BlockSignal.WaitOne(VERY_LONG_TIMEOUT);
				client1Done = true;
				client1CompleteSignal.Set();
			});
			extraClientHandler.Expect(x => x.Run()).WhenCalled(m => {
				client2Done = true;
				client2CompleteSignal.Set();
			});

			target.AcceptOne();
			Assert.IsFalse(client1Done);
			Assert.IsFalse(client2Done);
			target.AcceptOne();
			client2CompleteSignal.WaitOne(VERY_LONG_TIMEOUT);
			Assert.IsFalse(client1Done);
			Assert.IsTrue(client2Done);
			client1BlockSignal.Set();
			client1CompleteSignal.WaitOne(VERY_LONG_TIMEOUT);
			Assert.IsTrue(client1Done);

			clientHandler.VerifyAllExpectations();
			extraClientHandler.VerifyAllExpectations();
		}
	}
}
