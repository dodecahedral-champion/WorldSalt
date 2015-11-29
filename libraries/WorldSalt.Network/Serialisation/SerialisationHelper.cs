namespace WorldSalt.Network.Serialisation {
	using System;
	using System.Collections.Generic;
	using System.Linq;
	using System.Text;
	using Index = System.UInt64;

	public static class SerialisationHelper {
		public delegate Index Loader<T>(Byte[] bytes, Index startIndex, out T value);
		public delegate IEnumerable<Byte[]> Serialiser<T>(T self);

		public static IEnumerable<Byte[]> Serialise(this string self) {
			if(string.IsNullOrEmpty(self)) {
				return ((UInt64)0).Serialise();
			}

			var length = (UInt64)self.Length;
			var stringBytes = Encoding.UTF8.GetBytes(self);
			return length.Serialise().Concat(new[] { stringBytes });
		}

		public static IEnumerable<Byte[]> Serialise(this UInt64 self) {
			return Enumerable.Repeat(SerialiseUnsignedInteger(self, 8).ToArray(), 1);
		}

		public static IEnumerable<Byte[]> Serialise<T>(this IEnumerable<T> self, Serialiser<T> itemSerialiser) {
			if(self == null) {
				return ((UInt64)0).Serialise();
			}

			var fragments = self.Select(x => itemSerialiser(x)).ToList();
			var length = (UInt64)fragments.LongCount();
			return length.Serialise().Concat(fragments.SelectMany(x => x));
		}

		public static Index LoadString(Byte[] bytes, Index startIndex, out string value) {
			UInt64 length;
			var index = LoadU64(bytes, startIndex, out length);
			Byte[] stringBytes;
			index = ReadByteSequence(bytes, index, out stringBytes, length);
			value = Encoding.UTF8.GetString(stringBytes);
			return index;
		}

		public static Index LoadU64(Byte[] bytes, Index startIndex, out UInt64 value) {
			return ReadUnsignedInteger(bytes, startIndex, out value, 8);
		}

		public static Index LoadList<TItem>(Byte[] bytes, Index startIndex, out IList<TItem> value, Loader<TItem> itemLoader) {
			UInt64 length;
			var index = LoadU64(bytes, startIndex, out length);
			return ReadSequence(bytes, index, out value, length, itemLoader);
		}

		public static void AssertEnd(Byte[] bytes, Index index) {
			var bytesRemaining = (UInt64)bytes.Length - index;
			if(bytesRemaining != 0) {
				throw new FormatException(string.Format("expected end of buffer: bytes remaining = {0}", bytesRemaining));
			}
		}

		public static Byte[] ConcatenateBuffers(this IEnumerable<Byte[]> buffers) {
			var buffersList = buffers.ToList();
			var totalLength = buffersList.Aggregate((long)0, (len, buffer) => len + buffer.LongLength);
			var combinedBuffer = new Byte[totalLength];
			long offset = 0;
			foreach(var buffer in buffersList) {
				buffer.CopyTo(combinedBuffer, offset);
				offset += buffer.LongLength;
			}

			return combinedBuffer;
		}

		private static Index ReadByteSequence(Byte[] bytes, Index startIndex, out Byte[] sequence, UInt64 numBytes) {
			var desiredMinLength = startIndex + numBytes;
			if(desiredMinLength > (UInt64)bytes.LongLength) {
				throw new FormatException(String.Format("cannot read sequence of {0} bytes; expected at least {1} bytes in buffer but found only {2}", numBytes, desiredMinLength, bytes.LongLength));
			}

			var working = new byte[numBytes];
			for (UInt64 i = 0; i < numBytes; i++) {
				working[i] = bytes[i + startIndex];
			}

			sequence = working;
			return startIndex + numBytes;
		}

		private static Index ReadSequence<TItem>(Byte[] bytes, Index startIndex, out IList<TItem> sequence, UInt64 numItems, Loader<TItem> itemLoader) {
			var working = new List<TItem>((int)numItems);
			var index = startIndex;
			TItem item;
			for (UInt64 i = 0; i < numItems; i++) {
				index = itemLoader(bytes, index, out item);
				working.Add(item);
			}

			sequence = working;
			return index;
		}

		private static Index ReadUnsignedInteger(Byte[] bytes, Index startIndex, out UInt64 value, UInt16 integerSizeBytes) {
			Byte[] sequence;
			var index = ReadByteSequence(bytes, startIndex, out sequence, integerSizeBytes);
			if(sequence.Length != integerSizeBytes) {
				throw new FormatException(string.Format("could not read u{1} starting at {0}", startIndex, integerSizeBytes*8));
			}

			value = sequence.Reverse().Aggregate((UInt64)0, (working, b) => working * 256 + b);
			return index;
		}

		private static IEnumerable<Byte> SerialiseUnsignedInteger(UInt64 value, UInt16 integerSizeBytes) {
			foreach(var i in Enumerable.Range(0, integerSizeBytes)) {
				var b = (Byte)(value % 256);
				yield return b;
				value = (value - b) / 256;
			}
		}
	}
}

