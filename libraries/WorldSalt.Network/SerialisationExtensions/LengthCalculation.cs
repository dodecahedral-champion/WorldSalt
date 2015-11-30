namespace WorldSalt.Network.SerialisationExtensions {
	using System;
	using System.Collections.Generic;
	using System.Linq;
	using System.Text;
	using Length = System.UInt32;
	using CountSpecifier = System.UInt32;

	public static class LengthCalculation {
		public static Length SerialisationLength(this string self) {
			return sizeof(CountSpecifier) + (Length)Encoding.UTF8.GetByteCount(self);
		}

		public static Length SerialisationLength(this UInt64 self) {
			return sizeof(UInt64);
		}

		public static Length SerialisationLength(this Guid self) {
			return 16;
		}

		public static Length SerialisationLength<T>(this IEnumerable<T> self, Length itemLength) {
			return (Length)self.Count() * itemLength;
		}

		public static Length SerialisationLength<T>(this IEnumerable<T> self, Func<T, Length> itemLengthCalculator) {
			return sizeof(CountSpecifier) + self.Aggregate((Length)0, (len, item) => len + itemLengthCalculator(item));
		}
	}
}
