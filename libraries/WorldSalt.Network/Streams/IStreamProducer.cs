namespace WorldSalt.Network.Streams {
	public interface IStreamProducer<out T> : IStreamCloseable {
		T Take();
	}
}
