namespace WorldSalt.Network.Serialisation {
	using System;
	using System.Collections.Generic;
	using System.Linq;
	using System.Text;

	public static class Serialisation {
		public delegate IEnumerable<Byte[]> Serialiser<T>(T self);

		public static IEnumerable<Byte[]> Serialise(this string self) {
			if(string.IsNullOrEmpty(self)) {
				return ((UInt32)0).Serialise();
			}

			var stringBytes = Encoding.UTF8.GetBytes(self);
			var length = (UInt32)stringBytes.Length;
			return length.Serialise().Concat(new[] { stringBytes });
		}

		public static IEnumerable<Byte[]> Serialise(this UInt64 self) {
			return Enumerable.Repeat(SerialiseUnsignedInteger(self, 8).ToArray(), 1);
		}

		public static IEnumerable<Byte[]> Serialise(this UInt32 self) {
			return Enumerable.Repeat(SerialiseUnsignedInteger(self, 4).ToArray(), 1);
		}

		public static IEnumerable<Byte[]> Serialise<T>(this IEnumerable<T> self, Serialiser<T> itemSerialiser) {
			if(self == null) {
				return ((UInt32)0).Serialise();
			}

			var fragments = self.Select(x => itemSerialiser(x)).ToList();
			var length = (UInt32)fragments.LongCount();
			return length.Serialise().Concat(fragments.SelectMany(x => x));
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

		private static IEnumerable<Byte> SerialiseUnsignedInteger(UInt64 value, UInt16 integerSizeBytes) {
			foreach(var i in Enumerable.Range(0, integerSizeBytes)) {
				var b = (Byte)(value % 256);
				yield return b;
				value = (value - b) / 256;
			}
		}
	}
}
