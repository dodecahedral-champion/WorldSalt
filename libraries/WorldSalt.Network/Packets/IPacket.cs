namespace WorldSalt.Network.Packets {
	using WorldSalt.Network.Direction;

	public interface IUntypedPacket<TDirection> where TDirection : IDirection {
		IRawPayload<TDirection> Payload { get; }
		ITypedPacket<TPayload, TDirection> ConvertToTyped<TPayload>() where TPayload : ITypedPayload<TDirection>, new();
	}

	public interface ITypedPacket<TDirection> where TDirection : IDirection {
		byte[] GetBytes();
	}

	public interface ITypedPacket<TPayload, TDirection> : ITypedPacket<TDirection> where TPayload : ITypedPayload<TDirection> where TDirection : IDirection {
		TPayload Payload { get; }
	}
}
