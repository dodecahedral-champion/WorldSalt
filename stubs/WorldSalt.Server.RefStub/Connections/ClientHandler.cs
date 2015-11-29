namespace WorldSalt.Server.RefStub.Connections {
	using System;
	using System.Linq;
	using System.Net.Sockets;
	using WorldSalt.Network;
	using WorldSalt.Network.Direction;
	using WorldSalt.Network.Packets;
	using WorldSalt.Network.Packets.Connection;

	public class ClientHandler : IClientHandler {
		IStreamDuplex<ITypedPacket<FromServer>, ITypedPacket<FromClient>> stream;
		IPacketFactory<FromServer> packetFactory;

		public ClientHandler(IPacketFactory<FromServer> packetFactory, IStreamDuplex<ITypedPacket<FromServer>, ITypedPacket<FromClient>> stream) {
			this.packetFactory = packetFactory;
			this.stream = stream;
		}

		public void Run() {
			var connectPacket = stream.Take();
			var connectPayload = connectPacket.Payload as ConnectPayload;
			if (connectPayload == null) {
				Console.WriteLine("[server] bad connect packet");
				throw new InvalidOperationException("bad connect packet");
			}

			var serverProtocol = ProtocolVersion.CURRENT;
			if(connectPayload.PreferredProtocol == serverProtocol || connectPayload.SupportedProtocols.Contains(serverProtocol)) {
				Console.WriteLine("[server] accepted connection");
				stream.Put(packetFactory.Create(new ConnectedPayload()));
			} else {
				Console.WriteLine("[server] rejected connection");
				stream.Put(packetFactory.Create(new UnsupportedProtocolVersionPayload(serverProtocol, Enumerable.Empty<UInt64>())));
				return;
			}

			Console.WriteLine("[server] kicked client");
			stream.Put(packetFactory.Create(new KickedPayload("you're done here")));
		}
	}
}
