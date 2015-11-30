namespace WorldSalt.Server.RefStub.Connections {
	using WorldSalt.Network.Direction;
	using WorldSalt.Network.Streams.Bytes;

	public interface IClientHandlerFactory {
		IClientHandler Create(IByteSink<FromServer> byteSink, IByteSource<FromClient> byteSource);
	}
}
