namespace WorldSalt.Network {
	public interface IStreamDuplex<T> : IStreamConsumer<T>, IStreamProducer<T> {
	}
}
