namespace WorldSalt.Network.Payloads.Ping {
	using System;
	using WorldSalt.Network.Direction;
	using WorldSalt.Network.SerialisationExtensions;

	public class ClientPingResponsePayload : BaseTypedPayload<FromServer> {
		public override byte Type { get { return 0x01; } }
		public override byte Subtype { get { return 0x01; } }

		public override uint Length {
			get { return RequestId.SerialisationLength(); }
		}

		public Guid RequestId { get; private set; }

		public ClientPingResponsePayload() : this(Guid.Empty) {
		}

		public ClientPingResponsePayload(Guid identifier) {
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
