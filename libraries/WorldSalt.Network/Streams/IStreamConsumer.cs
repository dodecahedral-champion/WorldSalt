namespace WorldSalt.Network.Streams {
	public interface IStreamConsumer<T> : IStreamCloseable {
		void Put(T value);
	}
}
