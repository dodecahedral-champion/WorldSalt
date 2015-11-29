namespace WorldSalt.Network.Packets {
	using WorldSalt.Network.Direction;

	public interface IUntypedPacket<TDirection> where TDirection : IDirection {
		byte Type { get; }
		byte Subtype { get; }
		IRawPayload<TDirection> Payload { get; }
	}

	public interface ITypedPacket<TDirection> where TDirection : IDirection {
		ITypedPayload<TDirection> Payload { get; }
		byte[] GetBytes();
	}

	public interface ITypedPacket<TPayload, TDirection> : ITypedPacket<TDirection> where TPayload : ITypedPayload<TDirection> where TDirection : IDirection {
		new TPayload Payload { get; }
	}
}
