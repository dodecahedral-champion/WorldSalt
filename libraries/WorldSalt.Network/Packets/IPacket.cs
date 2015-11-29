namespace WorldSalt.Network.Packets {
	public interface IPacket {
	}

	public interface IPacket<TPayload> : IPacket where TPayload : IPayload {
	}
}
