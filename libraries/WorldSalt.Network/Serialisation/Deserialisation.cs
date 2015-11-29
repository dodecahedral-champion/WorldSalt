namespace WorldSalt.Network.Serialisation {
	using System;
	using System.Collections.Generic;
	using System.IO;
	using System.Linq;
	using System.Text;

	public static class Deserialisation {
		public delegate Stream Deserialiser<T>(Stream stream, out T value);

		public static Stream Deserialise(this Stream stream, out string value) {
			UInt32 length;
			stream = stream.Deserialise(out length);
			value = Encoding.UTF8.GetString(stream.ReadByteSequence(length));
			return stream;
		}

		public static Stream Deserialise(this Stream stream, out UInt32 value) {
			UInt64 bigValue;
			stream = stream.DeserialiseUnsignedInteger(out bigValue, 4);
			value = (UInt32)bigValue;
			return stream;
		}

		public static Stream Deserialise(this Stream stream, out UInt64 value) {
			return stream.DeserialiseUnsignedInteger(out value, 8);
		}

		public static Stream Deserialise<TItem>(this Stream stream, out IList<TItem> value, Deserialiser<TItem> deserialiser) {
			UInt32 length;
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

		private static Byte[] ReadByteSequence(this Stream stream, UInt32 numBytes) {
			var smallLength = (int)numBytes;
			var sequence = new Byte[smallLength];
			var numRead = stream.Read(sequence, 0, smallLength);
			if(numRead != smallLength) {
				throw new FormatException(String.Format("tried to read sequence of {0} bytes, but only found {1}", smallLength, numRead));
			}

			return sequence;
		}

		private static IEnumerable<TItem> ReadSequence<TItem>(this Stream stream, UInt32 numItems, Deserialiser<TItem> deserialiser) {
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
	}
}
