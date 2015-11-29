namespace WorldSalt.Network.Streams {
	public interface IStreamProducer<T> : IStreamCloseable {
		T Take();
	}
}
