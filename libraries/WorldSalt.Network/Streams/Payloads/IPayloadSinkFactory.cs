namespace WorldSalt.Network.Streams.Payloads {
	using WorldSalt.Network.Direction;
	using WorldSalt.Network.Payloads;
	using WorldSalt.Network.Streams.Bytes;

	public interface IPayloadSinkFactory<TDir> where TDir : IDirection {
		IStreamConsumer<ITypedPayload<TDir>> Create(IByteSink<TDir> byteSink);
	}
}
