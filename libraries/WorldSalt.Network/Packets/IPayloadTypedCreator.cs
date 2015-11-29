namespace WorldSalt.Network.Packets {
	using System;
	using WorldSalt.Network.Direction;

	public interface IPayloadTypedCreator<TDirection> where TDirection : IDirection {
		ITypedPayload<TDirection> Create(Byte packetType, Byte packetSubtype);
	}
}
