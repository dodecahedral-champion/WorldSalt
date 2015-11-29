namespace WorldSalt.Client.RefStub {
	using System;
	using System.Collections.Generic;
	using System.Linq;
	using WorldSalt.Network;
	using WorldSalt.Network.Direction;
	using WorldSalt.Network.Frames;
	using WorldSalt.Network.Payloads;
	using WorldSalt.Network.Streams;

	public class MainClass {
		private static int DEFAULT_PORT = 1117;

		public static void Main(string[] args) {
			var hostname = GetHostname(args);
			var port = GetPort(args);
			Console.WriteLine("[client] connecting to {0}:{1}", hostname, port);
			var frameFactory = new FrameFactory<FromClient>(new PayloadFactory<FromClient>(new PayloadTypedCreatorFromClient()));
			using (var client = new ClientProcess(frameFactory, new FrameStreamFactory(), hostname, port)) {
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
