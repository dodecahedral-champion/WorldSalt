namespace WorldSalt.Network.Payloads {
	using System;
	using WorldSalt.Network.Direction;

	public class PayloadFactory<TDirection> : IPayloadFactory<TDirection> where TDirection : IDirection {
		private readonly IPayloadTypedCreator<TDirection> payloadTypedCreator;

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

		public ITypedPayload<TDirection> ConvertPayload(IRawPayload<TDirection> untypedPayload, Byte type, Byte subtype) {
			var payload = CreatePayload(type, subtype);
			payload.SetBytes(untypedPayload.GetBytes());
			return payload;
		}

		private ITypedPayload<TDirection> CreatePayload(Byte type, Byte subtype) {
			return payloadTypedCreator.Create(type, subtype);
		}
	}
}
