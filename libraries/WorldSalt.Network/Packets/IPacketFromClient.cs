namespace WorldSalt.Network.Packets {
	public interface IPacketFromClient : IPacket {
	}

	public interface IPacketFromClient<T> : IPacket<T>, IPacketFromClient where T : IPayloadFromClient {
	}
}
