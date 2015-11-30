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
	using WorldSalt.Network.Streams.Frames;
	using WorldSalt.Network.Streams.Payloads;
	using WorldSalt.Server.RefStub.Connections;

	public class MainClass {
		private static int DEFAULT_PORT = 1117;

		public static void Main(string[] args) {
			var payloadFactoryS = new PayloadFactory<FromServer>(new PayloadTypedCreatorFromServer());
			var payloadFactoryC = new PayloadFactory<FromClient>(new PayloadTypedCreatorFromClient());
			var frameFactoryS = new FrameFactory<FromServer>(payloadFactoryS);
			var frameFactoryC = new FrameFactory<FromClient>(payloadFactoryC);
			var frameSinkFactory = new FrameSinkFactory<FromServer>();
			var frameSourceFactory = new FrameSourceFactory<FromClient>(payloadFactoryC, frameFactoryC);
			var sinkFactory = new PayloadSinkFactory<FromServer>(frameSinkFactory, frameFactoryS);
			var sourceFactory = new PayloadSourceFactory<FromClient>(frameSourceFactory);
			var clientHandlerFactory = new ClientHandlerFactory(sinkFactory, sourceFactory);
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
