namespace WorldSalt.Network.Frames {
	using System.Linq;
	using WorldSalt.Network.Direction;
	using WorldSalt.Network.Payloads;
	using WorldSalt.Network.SerialisationExtensions;

	internal class Frame<TPayload, TDirection> : ITypedFrame<TPayload, TDirection> where TPayload : ITypedPayload<TDirection> where TDirection : IDirection {
		public TPayload Payload { get; private set; }

		ITypedPayload<TDirection> ITypedFrame<TDirection>.Payload {
			get { return Payload; }
		}

		public Frame(TPayload payload) {
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
