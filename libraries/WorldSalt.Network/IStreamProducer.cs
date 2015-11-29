namespace WorldSalt.Network {
	public interface IStreamProducer<T> {
		T Take();
		void Close();
	}
}
