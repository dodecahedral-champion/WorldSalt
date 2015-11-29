namespace WorldSalt.Network.Payloads {
	public class UntypedPayload<TDirection> : IRawPayload<TDirection> {
		private byte[] rawBuffer;
		public uint Length {
			get { return (uint)rawBuffer.Length; }
		}

		public UntypedPayload() : this(new byte[0]) {
		}

		public UntypedPayload(byte[] bytes) {
			rawBuffer = bytes;
		}

		public void SetBytes(byte[] bytes) {
			rawBuffer = bytes;
		}

		public byte[] GetBytes() {
			return rawBuffer;
		}
	}
}
