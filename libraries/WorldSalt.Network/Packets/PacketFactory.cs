namespace WorldSalt.Network.Packets {
	using WorldSalt.Network.Direction;

	public class PacketFactory<TDirection> : IPacketFactory<TDirection> where TDirection : IDirection {
		public ITypedPacket<TPayload, TDirection> Create<TPayload>(TPayload payload) where TPayload : ITypedPayload<TDirection> {
			throw new System.NotImplementedException();
		}

		public ITypedPacket<TPayload, TDirection> Create<TPayload>(IUntypedPacket<TDirection> untypedPacket) where TPayload : ITypedPayload<TDirection>, new() {
			throw new System.NotImplementedException();
		}

		public TPayload ConvertPayload<TPayload>(IRawPayload<TDirection> untypedPayload) where TPayload : ITypedPayload<TDirection>, new() {
			var payload = new TPayload();
			payload.SetBytes(untypedPayload.GetBytes());
			return payload;
		}
	}
}
