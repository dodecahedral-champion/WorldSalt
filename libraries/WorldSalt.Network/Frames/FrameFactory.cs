namespace WorldSalt.Network.Frames {
	using System;
	using WorldSalt.Network.Direction;
	using WorldSalt.Network.Payloads;

	public class FrameFactory<TDirection> : IFrameFactory<TDirection> where TDirection : IDirection {
		private readonly IPayloadFactory<TDirection> payloadFactory;

		public FrameFactory(IPayloadFactory<TDirection> payloadFactory) {
			this.payloadFactory = payloadFactory;
		}

		public ITypedFrame<TPayload, TDirection> Create<TPayload>(TPayload payload) where TPayload : ITypedPayload<TDirection> {
			return new Frame<TPayload, TDirection>(payload);
		}

		public ITypedFrame<TPayload, TDirection> ConvertToTyped<TPayload>(IUntypedFrame<TDirection> untypedFrame) where TPayload : ITypedPayload<TDirection>, new() {
			var typedPayload = payloadFactory.ConvertPayload<TPayload>(untypedFrame.Payload);
			return new Frame<TPayload, TDirection>(typedPayload);
		}

		public IUntypedFrame<TDirection> CreateUntyped(Byte type, Byte subtype, IRawPayload<TDirection> payload) {
			return new UntypedFrame<TDirection>(type, subtype, payload);
		}
	}
}
