namespace WorldSalt.Network.Packets {
	public interface IPacketFromServer : IPacket {
	}

	public interface IPacketFromServer<T> : IPacket<T>, IPacketFromServer where T : IPayloadFromServer {
	}
}
