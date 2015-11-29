namespace WorldSalt.Network.Packets {
	public class UntypedPayload<TDirection> : IRawPayload<TDirection> {
		private byte[] rawBuffer;
		public uint Length {
			get { return (uint)rawBuffer.Length; }
		}

		public UntypedPayload() {
			rawBuffer = new byte[0];
		}

		public void SetBytes(byte[] bytes) {
			rawBuffer = bytes;
		}

		public byte[] GetBytes() {
			return rawBuffer;
		}
	}
}
