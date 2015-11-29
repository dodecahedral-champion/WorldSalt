namespace WorldSalt.Network {
	public interface IStreamConsumer<T> {
		void Put(T value);
	}
}
