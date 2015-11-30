namespace WorldSalt.Network.Streams.Payloads {
	using WorldSalt.Network.Direction;
	using WorldSalt.Network.Payloads;
	using WorldSalt.Network.Streams.Bytes;

	public interface IPayloadSourceFactory<TDir> where TDir : IDirection {
		IStreamProducer<ITypedPayload<TDir>> Create(IByteSource<TDir> byteSource);
	}
}
