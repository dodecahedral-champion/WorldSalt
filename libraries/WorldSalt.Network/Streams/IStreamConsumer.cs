namespace WorldSalt.Network.Streams {
	public interface IStreamConsumer<in T> : IStreamCloseable {
		void Put(T value);
	}
}
