namespace WorldSalt.Server.RefStub {
	using System;
	using System.Collections.Generic;
	using System.Linq;
	using System.Threading;
	using WorldSalt.Network;
	using WorldSalt.Network.Direction;
	using WorldSalt.Network.Frames;
	using WorldSalt.Network.Payloads;
	using WorldSalt.Network.Streams;
	using WorldSalt.Server.RefStub.Connections;

	public class MainClass {
		private static int DEFAULT_PORT = 1117;

		public static void Main(string[] args) {
			var frameFactory = new FrameFactory<FromServer>(new PayloadFactory<FromServer>(new PayloadTypedCreatorFromServer()));
			var clientHandlerFactory = new ClientHandlerFactory(frameFactory, new FrameStreamFactory());
			var port = GetPort(args);
			Console.WriteLine("[server] listening on port {0}...", port);
			using (var connectionFoyer = new ConnectionFoyer(clientHandlerFactory, port)) {
				var server = new ServerProcess(connectionFoyer);
				while (true) {
					server.AcceptOne();
				}
			}
		}

		private static int GetPort(IEnumerable<string> args) {
			return args.Select(ParsePort).Concat(Enumerable.Repeat(DEFAULT_PORT, 1)).ElementAt(0);
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
