namespace WorldSalt.Network.Payloads.Connection {
	using System;
	using WorldSalt.Network.Direction;
	using WorldSalt.Network.SerialisationExtensions;

	public class ConnectedPayload : BaseTypedPayload<FromServer> {
		public override byte Type { get { return 0x00; } }
		public override byte Subtype { get { return 0x01; } }

		public override uint Length {
			get { return Protocol.SerialisationLength(); }
		}

		public UInt64 Protocol { get; private set; }

		public ConnectedPayload() : this(ProtocolVersion.CURRENT) {
		}

		public ConnectedPayload(UInt64 protocol) {
			Protocol = protocol;
		}

		public override void SetBytes(byte[] bytes) {
			UInt64 protocol;

			MakeDirectedByteStream(bytes)
				.Deserialise(out protocol)
				.AssertEnd();

			Protocol = protocol;
		}

		public override byte[] GetBytes() {
			return Protocol.Serialise().ConcatenateBuffers();
		}

	}
}
