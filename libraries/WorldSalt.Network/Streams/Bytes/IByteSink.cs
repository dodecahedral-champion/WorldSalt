namespace WorldSalt.Network.Streams.Bytes {
	using System;
	using WorldSalt.Network.Direction;

	public interface IByteSink<TDirection> : IStreamCloseable where TDirection : IDirection {
		void Put(Byte item);
		void Put(Byte[] bytes);
	}
}
