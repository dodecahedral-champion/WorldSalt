namespace WorldSalt.Client.RefStub {
	using System;
	using System.Collections.Generic;
	using System.Linq;
	using WorldSalt.Network;
	using WorldSalt.Network.Direction;
	using WorldSalt.Network.Frames;
	using WorldSalt.Network.Payloads;
	using WorldSalt.Network.Streams;
	using WorldSalt.Network.Streams.Frames;
	using WorldSalt.Network.Streams.Payloads;

	public class MainClass {
		private static int DEFAULT_PORT = 1117;

		public static void Main(string[] args) {
			var hostname = GetHostname(args);
			var port = GetPort(args);
			Console.WriteLine("[client] connecting to {0}:{1}", hostname, port);
			var payloadFactoryC = new PayloadFactory<FromClient>(new PayloadTypedCreatorFromClient());
			var payloadFactoryS = new PayloadFactory<FromServer>(new PayloadTypedCreatorFromServer());
			var frameFactoryC = new FrameFactory<FromClient>(payloadFactoryC);
			var frameFactoryS = new FrameFactory<FromServer>(payloadFactoryS);
			var frameSourceFactory = new FrameSourceFactory<FromServer>(payloadFactoryS, frameFactoryS);
			var frameSinkFactory = new FrameSinkFactory<FromClient>();
			var sinkFactory = new PayloadSinkFactory<FromClient>(frameSinkFactory, frameFactoryC);
			var sourceFactory = new PayloadSourceFactory<FromServer>(frameSourceFactory);
			using (var client = new ClientProcess(sinkFactory, sourceFactory, hostname, port)) {
				client.Connect("Fred", ProtocolVersion.CURRENT);
				client.Disconnect();
			}

			Console.WriteLine("[client] done.");
		}

		private static string GetHostname(IEnumerable<string> args) {
			return args.Concat(Enumerable.Repeat("localhost", 1)).ElementAt(0);
		}

		private static int GetPort(IEnumerable<string> args) {
			return args.Select(ParsePort).Concat(Enumerable.Repeat(DEFAULT_PORT, 2)).ElementAt(1);
		}

		private static int ParsePort(string text) {
			int port;
			if(int.TryParse(text, out port)) {
				return port;
			} else {
				return DEFAULT_PORT;
			}
		}
	}
}
