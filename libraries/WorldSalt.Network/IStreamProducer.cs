namespace WorldSalt.Network {
	using System;

	public interface IStreamProducer<T> {
		T Take();
	}
}
