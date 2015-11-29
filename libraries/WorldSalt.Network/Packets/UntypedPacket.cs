namespace WorldSalt.Network.Packets {
	using WorldSalt.Network.Direction;

	public class UntypedPacket<TDirection> : IUntypedPacket<TDirection> where TDirection : IDirection {
		public IRawPayload<TDirection> Payload { get; private set; }

		public UntypedPacket() {
			Payload = new UntypedPayload<TDirection>();
		}

		public ITypedPacket<TPayload, TDirection> ConvertToTyped<TPayload>() where TPayload : ITypedPayload<TDirection>, new() {
			var payload = new TPayload();
			payload.SetBytes(Payload.GetBytes());
			return new Packet<TPayload, TDirection>(payload);
		}
	}
}
