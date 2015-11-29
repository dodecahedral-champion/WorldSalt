namespace WorldSalt.Network.Packets {
	using System;
	using WorldSalt.Network.Direction;
	using WorldSalt.Network.Packets.Connection;

	public class PayloadFactory<TDirection> : IPayloadFactory<TDirection> where TDirection : IDirection {
		private IPayloadTypedCreator<TDirection> payloadTypedCreator;

		public PayloadFactory(IPayloadTypedCreator<TDirection> payloadTypedCreator) {
			this.payloadTypedCreator = payloadTypedCreator;
		}

		public IRawPayload<TDirection> CreateUntypedPayload(Byte[] bytes) {
			return new UntypedPayload<TDirection>(bytes);
		}

		public TPayload ConvertPayload<TPayload>(IRawPayload<TDirection> untypedPayload) where TPayload : ITypedPayload<TDirection>, new() {
			var payload = new TPayload();
			payload.SetBytes(untypedPayload.GetBytes());
			return payload;
		}

		public ITypedPayload<TDirection> ConvertPayload(IRawPayload<TDirection> untypedPayload, Byte packetType, Byte packetSubtype) {
			var payload = CreatePayload(packetType, packetSubtype);
			payload.SetBytes(untypedPayload.GetBytes());
			return payload;
		}

		private ITypedPayload<TDirection> CreatePayload(byte packetType, byte packetSubtype) {
			return payloadTypedCreator.Create(packetType, packetSubtype);
		}
	}
}
