namespace WorldSalt.UnitTests.Server.RefStub.Connections {
	using NUnit.Framework;
	using Rhino.Mocks;
	using WorldSalt.Network.Direction;
	using WorldSalt.Network.Payloads;
	using WorldSalt.Network.Streams;
	using WorldSalt.Network.Streams.Bytes;
	using WorldSalt.Network.Streams.Payloads;
	using WorldSalt.Server.RefStub.Connections;

	[TestFixture]
	public class ClientHandlerFactoryUnitTest {
		private IPayloadSinkFactory<FromServer> sinkFactory;
		private IPayloadSourceFactory<FromClient> sourceFactory;

		private ClientHandlerFactory target;

		[SetUp]
		public void Setup() {
			sinkFactory = MockRepository.GenerateMock<IPayloadSinkFactory<FromServer>>();
			sourceFactory = MockRepository.GenerateMock<IPayloadSourceFactory<FromClient>>();

			target = new ClientHandlerFactory(sinkFactory, sourceFactory);
		}

		[Test]
		public void ShouldCreateStreamWhenCreatingClientHandler() {
			var byteSink = MockRepository.GenerateMock<IByteSink<FromServer>>();
			var byteSource = MockRepository.GenerateMock<IByteSource<FromClient>>();
			var sink = MockRepository.GenerateMock<IStreamConsumer<ITypedPayload<FromServer>>>();
			var source = MockRepository.GenerateMock<IStreamProducer<ITypedPayload<FromClient>>>();
			sinkFactory.Expect(x => x.Create(byteSink)).Return(sink);
			sourceFactory.Expect(x => x.Create(byteSource)).Return(source);
			sink.Expect(x => x.Close());
			source.Expect(x => x.Close());

			var clientHandler = target.Create(byteSink, byteSource);
			clientHandler.Close();

			Assert.IsNotNull(clientHandler);
			sinkFactory.VerifyAllExpectations();
			sourceFactory.VerifyAllExpectations();
		}
	}
}
