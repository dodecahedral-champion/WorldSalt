namespace WorldSalt.Network.Packets {
	using System;
	using WorldSalt.Network.Direction;
	using WorldSalt.Network.Packets.Connection;

	public class PacketFactory<TDirection> : IPacketFactory<TDirection> where TDirection : IDirection {
		private IPayloadFactory<TDirection> payloadFactory;

		public PacketFactory(IPayloadFactory<TDirection> payloadFactory) {
			this.payloadFactory = payloadFactory;
		}

		public ITypedPacket<TPayload, TDirection> Create<TPayload>(TPayload payload) where TPayload : ITypedPayload<TDirection> {
			return new Packet<TPayload, TDirection>(payload);
		}

		public ITypedPacket<TPayload, TDirection> Create<TPayload>(IUntypedPacket<TDirection> untypedPacket) where TPayload : ITypedPayload<TDirection>, new() {
			return new Packet<TPayload, TDirection>(payloadFactory.ConvertPayload<TPayload>(untypedPacket.Payload));
		}

		public IUntypedPacket<TDirection> CreateUntyped(Byte type, Byte subtype, IRawPayload<TDirection> payload) {
			return new UntypedPacket<TDirection>(type, subtype, payload);
		}
	}
}
