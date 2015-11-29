namespace WorldSalt.Network.SerialisationExtensions {
	using System;
	using System.Collections.Generic;
	using System.Linq;
	using System.Text;

	public static class LengthCalculation {
		public static UInt32 SerialisationLength(this string self) {
			return sizeof(UInt64) + (UInt32)Encoding.UTF8.GetByteCount(self);
		}

		public static UInt32 SerialisationLength(this UInt64 self) {
			return sizeof(UInt64);
		}

		public static UInt32 SerialisationLength<T>(this IEnumerable<T> self, UInt32 itemLength) {
			return (UInt32)self.Count() * itemLength;
		}

		public static UInt32 SerialisationLength<T>(this IEnumerable<T> self, Func<T, UInt32> itemLengthCalculator) {
			return self.Aggregate((UInt32)0, (len, item) => len + itemLengthCalculator(item));
		}
	}
}
