namespace WorldSalt.Network.Packets {
	using System.Linq;
	using WorldSalt.Network.Direction;
	using WorldSalt.Network.SerialisationExtensions;

	internal class Packet<TPayload, TDirection> : ITypedPacket<TPayload, TDirection> where TPayload : ITypedPayload<TDirection> where TDirection : IDirection {
		public TPayload Payload { get; private set; }

		ITypedPayload<TDirection> ITypedPacket<TDirection>.Payload {
			get { return Payload; }
		}

		public Packet(TPayload payload) {
			Payload = payload;
		}

		public byte[] GetBytes() {
			return Payload.Length.Serialise()
				.Concat(Payload.Type.Serialise())
				.Concat(Payload.Subtype.Serialise())
				.Concat(Payload.GetBytes().Serialise())
				.ConcatenateBuffers();
		}
	}
}
