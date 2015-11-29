namespace WorldSalt.Network.Packets {
	using System;
	using WorldSalt.Network.Direction;

	public interface IPacketFactory<TDirection> where TDirection : IDirection {
		ITypedPacket<TPayload, TDirection> Create<TPayload>(TPayload payload) where TPayload : ITypedPayload<TDirection>;

		ITypedPacket<TPayload, TDirection> Create<TPayload>(IUntypedPacket<TDirection> untypedPacket) where TPayload : ITypedPayload<TDirection>, new();

		IUntypedPacket<TDirection> CreateUntyped(Byte type, Byte subtype, IRawPayload<TDirection> payload);
	}
}
