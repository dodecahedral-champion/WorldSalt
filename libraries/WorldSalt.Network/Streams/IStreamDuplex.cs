namespace WorldSalt.Network.Streams {
	public interface IStreamDuplex<TConsume, TProduce> : IStreamConsumer<TConsume>, IStreamProducer<TProduce> {
	}
}
