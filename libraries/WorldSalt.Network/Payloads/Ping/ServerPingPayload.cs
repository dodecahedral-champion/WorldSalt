namespace WorldSalt.Network.Payloads.Ping {
	using System;
	using WorldSalt.Network.Direction;
	using WorldSalt.Network.SerialisationExtensions;

	public class ServerPingPayload : BaseTypedPayload<FromServer> {
		public override byte Type { get { return 0x01; } }
		public override byte Subtype { get { return 0x02; } }

		public override uint Length {
			get { return Id.SerialisationLength(); }
		}

		public Guid Id { get; private set; }

		public ServerPingPayload() : this(Guid.Empty) {
		}

		public ServerPingPayload(Guid identifier) {
			Id = identifier;
		}

		public override void SetBytes(byte[] bytes) {
			Guid identifier;

			MakeDirectedByteStream(bytes)
				.Deserialise(out identifier)
				.AssertEnd();

			Id = identifier;
		}

		public override byte[] GetBytes() {
			return Id.Serialise()
				.ConcatenateBuffers();
		}
	}
}
