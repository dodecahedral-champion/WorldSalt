namespace WorldSalt.Client.RefStub {
	using System;
	using System.Linq;
	using System.Net.Sockets;
	using WorldSalt.Network;
	using WorldSalt.Network.Direction;
	using WorldSalt.Network.Packets;
	using WorldSalt.Network.Packets.Connection;
	using WorldSalt.Network.Streams;

	public class ClientProcess {
		private IStreamDuplex<ITypedPacket<FromClient>, ITypedPacket<FromServer>> stream;
		private IPacketFactory<FromClient> packetFactory;
		public ClientProcess(IPacketFactory<FromClient> packetFactory, IPacketStreamFactory packetStreamFactory, string hostname, int port) {
			stream = packetStreamFactory.CreateDuplexForClient(new TcpClient(hostname, port));
			this.packetFactory = packetFactory;
		}

		public void Connect(string username, UInt64 protocolVersion) {
			Console.WriteLine("[client] connecting...");
			stream.Put(packetFactory.Create(new ConnectPayload(username, protocolVersion, Enumerable.Empty<UInt64>())));
			var connectResponse = stream.Take();
			if(connectResponse.Payload as ConnectedPayload != null) {
				Console.WriteLine("[client] connected okay!");
				return;
			} else {
				var rejected = connectResponse.Payload as UnsupportedProtocolVersionPayload;
				var message = string.Format("preferred protocol {0}", rejected.PreferredProtocol);
				if (rejected.SupportedProtocols.Any()) {
					message = string.Format("{0} (or any of: {1})", message, string.Join(",", rejected.SupportedProtocols.Select(x => x.ToString())));
				}
				Console.WriteLine("[client] rejected: {0}", message);

				throw new InvalidOperationException(message);
			}
		}
	}
}
