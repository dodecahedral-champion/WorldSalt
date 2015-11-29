namespace WorldSalt.Network.Packets {
	using WorldSalt.Network.Direction;

	public class Packet<TPayload, TDirection> : ITypedPacket<TPayload, TDirection> where TPayload : ITypedPayload<TDirection> where TDirection : IDirection {
		public TPayload Payload { get; private set; }

		public Packet(TPayload payload) {
			Payload = payload;
		}

		public byte[] GetBytes() {
			throw new System.NotImplementedException();
		}
	}
}
