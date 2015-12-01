namespace WorldSalt.Network.Frames {
	using WorldSalt.Network.Direction;
	using WorldSalt.Network.Payloads;

	public interface IUntypedFrame<TDirection> where TDirection : IDirection {
		byte Type { get; }
		byte Subtype { get; }
		IRawPayload<TDirection> Payload { get; }
	}

	public interface ITypedFrame<TDirection> where TDirection : IDirection {
		ITypedPayload<TDirection> Payload { get; }
		byte[] GetBytes();
	}

	public interface ITypedFrame<out TPayload, TDirection> : ITypedFrame<TDirection> where TPayload : ITypedPayload<TDirection> where TDirection : IDirection {
		new TPayload Payload { get; }
	}
}
