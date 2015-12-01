namespace WorldSalt.Network.Payloads {
	using System;
	using WorldSalt.Network.Direction;

    public interface IRawPayload<TDirection> where TDirection : IDirection {
		UInt32 Length { get; }

		void SetBytes(byte[] bytes);
		byte[] GetBytes();
	}

	public interface ITypedPayload<TDirection> where TDirection : IDirection {
        byte Type { get; }
		byte Subtype { get; }
		UInt32 Length { get; }

		void SetBytes(byte[] bytes);
		byte[] GetBytes();
	}
}
