namespace WorldSalt.Network {
	using System;

	public interface IStreamProducer<T> {
		bool TryTake(out T value);
	}
}
