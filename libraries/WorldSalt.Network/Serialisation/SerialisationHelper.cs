namespace WorldSalt.Network.Serialisation {
	using System;
	using System.Collections.Generic;
	using System.IO;
	using System.Linq;
	using System.Text;

	public static class SerialisationHelper {
		public delegate Stream Deserialiser<T>(Stream stream, out T value);
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

		public static Stream Deserialise(this Stream stream, out string value) {
			UInt64 length;
			stream = stream.Deserialise(out length);
			value = Encoding.UTF8.GetString(stream.ReadByteSequence(length));
			return stream;
		}

		public static Stream Deserialise(this Stream stream, out UInt64 value) {
			return stream.DeserialiseUnsignedInteger(out value, 8);
		}

		public static Stream Deserialise<TItem>(this Stream stream, out IList<TItem> value, Deserialiser<TItem> deserialiser) {
			UInt64 length;
			stream = stream.Deserialise(out length);
			value = stream.ReadSequence<TItem>(length, deserialiser).ToList();
			return stream;
		}

		public static void AssertEnd(this Stream stream) {
			var oldPosition = stream.Position;
			var endPosition = stream.Seek(0, SeekOrigin.End);
			if(endPosition != oldPosition) {
				throw new FormatException("expected end of buffer");
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

		private static Byte[] ReadByteSequence(this Stream stream, UInt64 numBytes) {
			var smallLength = (int)numBytes;
			var sequence = new Byte[smallLength];
			var numRead = stream.Read(sequence, 0, smallLength);
			if(numRead != smallLength) {
				throw new FormatException(String.Format("tried to read sequence of {0} bytes, but only found {1}", smallLength, numRead));
			}

			return sequence;
		}

		private static IEnumerable<TItem> ReadSequence<TItem>(this Stream stream, UInt64 numItems, Deserialiser<TItem> deserialiser) {
			foreach (var i in Enumerable.Repeat(0, (int)numItems)) {
				TItem item;
				stream = deserialiser(stream, out item);
				yield return item;
			}
		}

		private static Stream DeserialiseUnsignedInteger(this Stream stream, out UInt64 value, UInt16 integerSizeBytes) {
			value = stream.ReadByteSequence(integerSizeBytes).Reverse()
				.Aggregate((UInt64)0, (working, b) => working * 256 + b);
			return stream;
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

