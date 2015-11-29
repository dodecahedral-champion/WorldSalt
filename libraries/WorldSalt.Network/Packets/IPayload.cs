namespace WorldSalt.Network.Packets {
	using System;

	public interface IRawPayload<IDirection> {
		UInt32 Length { get; }

		void SetBytes(byte[] bytes);
		byte[] GetBytes();
	}

	public interface ITypedPayload<IDirection> {
		byte Type { get; }
		byte Subtype { get; }
		UInt32 Length { get; }

		void SetBytes(byte[] bytes);
		byte[] GetBytes();
	}
}
