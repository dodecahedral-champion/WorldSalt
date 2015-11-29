namespace WorldSalt.Network {
	public interface IStreamDuplex<TConsume, TProduce> : IStreamConsumer<TConsume>, IStreamProducer<TProduce> {
	}
}
