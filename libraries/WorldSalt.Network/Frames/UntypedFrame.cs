namespace WorldSalt.Network.Frames {
	using WorldSalt.Network.Direction;
	using WorldSalt.Network.Payloads;

	public class UntypedFrame<TDirection> : IUntypedFrame<TDirection> where TDirection : IDirection {
		public byte Type { get; private set; }
		public byte Subtype { get; private set; }
		public IRawPayload<TDirection> Payload { get; private set; }

		public UntypedFrame() : this(0x00, 0x00, new UntypedPayload<TDirection>()) {
		}

		public UntypedFrame(byte type, byte subtype, IRawPayload<TDirection> payload) {
			Type = type;
			Subtype = subtype;
			Payload = payload;
		}
	}
}
