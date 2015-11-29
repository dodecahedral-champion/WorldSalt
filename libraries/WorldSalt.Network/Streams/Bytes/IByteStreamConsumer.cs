namespace WorldSalt.Network {
	using System;
	using WorldSalt.Network.Direction;

	public interface IByteStreamConsumer<TDirection> where TDirection : IDirection {
		void Put(Byte item);
		void Put(Byte[] bytes);
	}
}
