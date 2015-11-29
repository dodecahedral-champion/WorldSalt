namespace WorldSalt.Network.Frames {
	using System;
	using WorldSalt.Network.Direction;
	using WorldSalt.Network.Payloads;

	public interface IFrameFactory<TDirection> where TDirection : IDirection {
		ITypedFrame<TPayload, TDirection> Create<TPayload>(TPayload payload) where TPayload : ITypedPayload<TDirection>;

		ITypedFrame<TPayload, TDirection> ConvertToTyped<TPayload>(IUntypedFrame<TDirection> untypedFrame) where TPayload : ITypedPayload<TDirection>, new();

		IUntypedFrame<TDirection> CreateUntyped(Byte type, Byte subtype, IRawPayload<TDirection> payload);
	}
}
