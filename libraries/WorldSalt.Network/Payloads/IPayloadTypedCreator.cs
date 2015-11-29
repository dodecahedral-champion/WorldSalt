namespace WorldSalt.Network.Payloads {
	using System;
	using WorldSalt.Network.Direction;

	public interface IPayloadTypedCreator<TDirection> where TDirection : IDirection {
		ITypedPayload<TDirection> Create(Byte type, Byte subtype);
	}
}
