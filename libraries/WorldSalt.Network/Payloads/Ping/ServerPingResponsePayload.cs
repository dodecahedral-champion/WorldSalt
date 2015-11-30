namespace WorldSalt.Network.Payloads.Ping {
	using System;
	using WorldSalt.Network.Direction;
	using WorldSalt.Network.SerialisationExtensions;

	public class ServerPingResponsePayload : BaseTypedPayload<FromClient> {
		public override byte Type { get { return 0x01; } }
		public override byte Subtype { get { return 0x03; } }

		public override uint Length {
			get { return RequestId.SerialisationLength(); }
		}

		public Guid RequestId { get; private set; }

		public ServerPingResponsePayload() : this(Guid.Empty) {
		}

		public ServerPingResponsePayload(Guid identifier) {
			RequestId = identifier;
		}

		public override void SetBytes(byte[] bytes) {
			Guid identifier;

			MakeDirectedByteStream(bytes)
				.Deserialise(out identifier)
				.AssertEnd();

			RequestId = identifier;
		}

		public override byte[] GetBytes() {
			return RequestId.Serialise()
				.ConcatenateBuffers();
		}
	}
}
