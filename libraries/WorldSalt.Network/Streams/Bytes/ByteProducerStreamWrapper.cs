namespace WorldSalt.Network.Streams.Bytes {
	using System;
	using System.IO;
	using WorldSalt.Network.Direction;

	public class ByteProducerStreamWrapper<TDirection> : IByteSource<TDirection> where TDirection : IDirection {
		protected Stream underlying;

		public bool IsAtEnd {
			get {
				var oldPosition = underlying.Position;
				var endPosition = underlying.Seek(0, SeekOrigin.End);
				if (endPosition == oldPosition) {
					return true;
				}

				underlying.Seek(oldPosition, SeekOrigin.Begin);
				return false;
			}
		}

		public ByteProducerStreamWrapper(Stream underlying) {
			this.underlying = underlying;
		}

		public void Dispose() {
			underlying.Close();
			underlying.Dispose();
		}

		public void Close() {
			underlying.Close();
		}

		public byte Read() {
			var value = underlying.ReadByte();
			if(value < 0) {
				throw new IOException("unable to read byte from stream");
			}

			return (byte)value;
		}

		public byte[] ReadSequence(uint desiredLength) {
			byte[] sequence;
			var actualReads = TryReadSequence(desiredLength, out sequence);
			if(actualReads != desiredLength) {
				throw new IOException(string.Format("unable to read {0} bytes; only {1} were available", desiredLength, actualReads));
			}

			return sequence;
		}

		public UInt32 TryReadSequence(UInt32 desiredLength, out byte[] sequence) {
			var smallLength = (int)desiredLength;
			var destination = new byte[smallLength];
			var actualReads = underlying.Read(destination, 0, smallLength);
			sequence = destination;
			return (UInt32)actualReads;
		}
	}
}
