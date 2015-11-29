namespace WorldSalt.Network.Packets {
	using WorldSalt.Network.Direction;

	public class UntypedPacket<TDirection> : IUntypedPacket<TDirection> where TDirection : IDirection {
		public IRawPayload<TDirection> Payload { get; private set; }

		public UntypedPacket() {
			Payload = new UntypedPayload<TDirection>();
		}
	}
}
