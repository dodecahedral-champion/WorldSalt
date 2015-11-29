namespace WorldSalt.Server.RefStub {
	using System;
	using System.Collections.Generic;
	using System.Linq;
	using System.Threading;
	using WorldSalt.Network;
	using WorldSalt.Server.RefStub.Connections;

	public class MainClass {
		private static int DEFAULT_PORT = 1117;

		public static void Main(string[] args) {
			var clientHandlerFactory = new ClientHandlerFactory(new PacketStreamFactory());
			var port = GetPort(args);
			Console.WriteLine("listening on port {0}...", port);
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
