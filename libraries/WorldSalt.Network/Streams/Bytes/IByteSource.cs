namespace WorldSalt.Network.Streams.Bytes {
	using System;
	using WorldSalt.Network.Direction;

	public interface IByteSource<TDirection> : IStreamCloseable where TDirection : IDirection {
		bool IsAtEnd { get; }

		byte Read();
		byte[] ReadSequence(UInt32 desiredLength);
		UInt32 TryReadSequence(UInt32 desiredLength, out byte[] sequence);
	}
}
