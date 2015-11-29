namespace WorldSalt.Client.RefStub {
	using System;
	using System.Linq;
	using System.Net.Sockets;
	using WorldSalt.Network;
	using WorldSalt.Network.Direction;
	using WorldSalt.Network.Frames;
	using WorldSalt.Network.Payloads.Connection;
	using WorldSalt.Network.Streams;

	public class ClientProcess {
		private IStreamDuplex<ITypedFrame<FromClient>, ITypedFrame<FromServer>> stream;
		private IFrameFactory<FromClient> frameFactory;
		public ClientProcess(IFrameFactory<FromClient> frameFactory, IFrameStreamFactory streamFactory, string hostname, int port) {
			stream = streamFactory.CreateDuplexForClient(new TcpClient(hostname, port));
			this.frameFactory = frameFactory;
		}

		public void Connect(string username, UInt64 protocolVersion) {
			Console.WriteLine("[client] connecting...");
			stream.Put(frameFactory.Create(new ConnectPayload(username, protocolVersion, Enumerable.Empty<UInt64>())));
			var connectResponse = stream.Take();
			CheckForUnsupportedVersion(connectResponse.Payload as UnsupportedProtocolVersionPayload);
			if(connectResponse.Payload as ConnectedPayload != null) {
				Console.WriteLine("[client] connected okay!");
				return;
			}
		}

		public void Disconnect() {
			Console.WriteLine("[client] disconnecting.");
			stream.Put(frameFactory.Create(new DisconnectPayload()));
			stream.Close();
		}

		private void CheckForUnsupportedVersion(UnsupportedProtocolVersionPayload payload) {
			if(payload == null) {
				return;
			}

			var message = string.Format("preferred protocol {0}", payload.PreferredProtocol);
			if (payload.SupportedProtocols.Any()) {
				message = string.Format("{0} (or any of: {1})", message, string.Join(",", payload.SupportedProtocols.Select(x => x.ToString())));
			}
			Console.WriteLine("[client] rejected: {0}", message);

			throw new InvalidOperationException(message);
		}
	}
}
