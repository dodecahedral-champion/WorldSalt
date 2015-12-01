namespace WorldSalt.Server.RefStub.Connections {
	using WorldSalt.Network.Direction;
	using WorldSalt.Network.Streams.Bytes;
	using WorldSalt.Network.Streams.Payloads;

	public class ClientHandlerFactory : IClientHandlerFactory {
		private readonly IPayloadSinkFactory<FromServer> sinkFactory;
		private readonly IPayloadSourceFactory<FromClient> sourceFactory;

		public ClientHandlerFactory(IPayloadSinkFactory<FromServer> sinkFactory, IPayloadSourceFactory<FromClient> sourceFactory) {
			this.sinkFactory = sinkFactory;
			this.sourceFactory = sourceFactory;
		}

		public IClientHandler Create(IByteSink<FromServer> byteSink, IByteSource<FromClient> byteSource) {
			var sink = sinkFactory.Create(byteSink);
			var source = sourceFactory.Create(byteSource);
			return new ClientHandler(sink, source);
		}
	}
}
