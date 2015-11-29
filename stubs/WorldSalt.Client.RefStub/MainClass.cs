namespace WorldSalt.Client.RefStub {
	using System.Collections.Generic;
	using System.Linq;
	using WorldSalt.Network;

	public class MainClass {
		private static int DEFAULT_PORT = 1117;

		public static void Main(string[] args) {
			var client = new ClientProcess(new PacketStreamFactory(), GetHostname(args), GetPort(args));
			client.Run();
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
