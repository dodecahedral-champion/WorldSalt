namespace WorldSalt.Network.Packets {
	using System;
	using WorldSalt.Network.Direction;

	public interface IPayloadFactory<TDirection> where TDirection : IDirection {
		IRawPayload<TDirection> CreateUntypedPayload(Byte[] bytes);

		TPayload ConvertPayload<TPayload>(IRawPayload<TDirection> untypedPayload) where TPayload : ITypedPayload<TDirection>, new();

		ITypedPayload<TDirection> ConvertPayload(IRawPayload<TDirection> untypedPayload, Byte packetType, Byte packetSubtype);
	}
}
