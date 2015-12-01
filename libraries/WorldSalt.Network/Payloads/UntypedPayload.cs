namespace WorldSalt.Network.Payloads {
    using WorldSalt.Network.Direction;

    public class UntypedPayload<TDirection> : IRawPayload<TDirection> where TDirection : IDirection {
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
