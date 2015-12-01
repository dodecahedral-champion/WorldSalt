namespace WorldSalt.Network.SerialisationExtensions {
	using System;
	using System.Collections.Generic;
	using System.Linq;
	using System.Text;
	using WorldSalt.Network.Direction;
	using WorldSalt.Network.Streams.Bytes;
	using CountSpecifier = System.UInt32;

	public static class Deserialisation {
		public delegate IByteSource<TDir> Deserialiser<T, TDir>(IByteSource<TDir> stream, out T value) where TDir : IDirection;

		public static IByteSource<TDir> Deserialise<TDir>(this IByteSource<TDir> stream, out Byte[] value, UInt32 sequenceLength) where TDir : IDirection {
			value = stream.ReadSequence(sequenceLength);
			return stream;
		}

		public static IByteSource<TDir> Deserialise<TDir>(this IByteSource<TDir> stream, out Byte value) where TDir : IDirection {
			value = stream.Read();
			return stream;
		}

		public static IByteSource<TDir> Deserialise<TDir>(this IByteSource<TDir> stream, out string value) where TDir : IDirection {
			CountSpecifier length;
			stream = stream.Deserialise(out length);
			value = Encoding.UTF8.GetString(stream.ReadSequence(length));
			return stream;
		}

		public static IByteSource<TDir> Deserialise<TDir>(this IByteSource<TDir> stream, out UInt32 value) where TDir : IDirection {
			UInt64 bigValue;
			stream = stream.DeserialiseUnsignedInteger(out bigValue, 4);
			value = (UInt32)bigValue;
			return stream;
		}

		public static IByteSource<TDir> Deserialise<TDir>(this IByteSource<TDir> stream, out UInt64 value) where TDir : IDirection {
			return stream.DeserialiseUnsignedInteger(out value, 8);
		}

		public static IByteSource<TDir> Deserialise<TDir>(this IByteSource<TDir> stream, out Guid value) where TDir : IDirection {
			value = new Guid(stream.ReadSequence(16));
			return stream;
		}

		public static IByteSource<TDir> Deserialise<TItem, TDir>(this IByteSource<TDir> stream, out IList<TItem> value, Deserialiser<TItem, TDir> deserialiser) where TDir : IDirection {
			CountSpecifier length;
			stream = stream.Deserialise(out length);
			value = stream.ReadSequence(length, deserialiser).ToList();
			return stream;
		}

		public static void AssertEnd<TDir>(this IByteSource<TDir> stream) where TDir : IDirection {
			if(!stream.IsAtEnd) {
				throw new FormatException("expected end of buffer");
			}
		}

		private static IEnumerable<TItem> ReadSequence<TItem, TDir>(this IByteSource<TDir> stream, CountSpecifier numItems, Deserialiser<TItem, TDir> deserialiser) where TDir : IDirection {
			foreach (var _ in Enumerable.Repeat(0, (int)numItems)) {
				TItem item;
				stream = deserialiser(stream, out item);
				yield return item;
			}
		}

		private static IByteSource<TDir> DeserialiseUnsignedInteger<TDir>(this IByteSource<TDir> stream, out UInt64 value, UInt16 integerSizeBytes) where TDir : IDirection {
			value = stream.ReadSequence(integerSizeBytes).Reverse()
				.Aggregate((UInt64)0, (working, b) => working * 256 + b);
			return stream;
		}
	}
}
