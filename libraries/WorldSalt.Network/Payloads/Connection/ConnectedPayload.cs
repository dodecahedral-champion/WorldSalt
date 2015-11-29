namespace WorldSalt.Network.Payloads.Connection {
	using System;
	using System.IO;
	using WorldSalt.Network.Direction;
	using WorldSalt.Network.SerialisationExtensions;

	public class ConnectedPayload : ITypedPayload<FromServer> {
		public byte Type { get { return 0x00; } }
		public byte Subtype { get { return 0x01; } }

		public uint Length {
			get { return Protocol.SerialisationLength(); }
		}

		public UInt64 Protocol { get; private set; }

		public ConnectedPayload() : this(ProtocolVersion.CURRENT) {
		}

		public ConnectedPayload(UInt64 protocol) {
			Protocol = protocol;
		}

		public void SetBytes(byte[] bytes) {
			UInt64 protocol;

			new MemoryStream(bytes)
				.Deserialise(out protocol)
				.AssertEnd();

			Protocol = protocol;
		}

		public byte[] GetBytes() {
			return Protocol.Serialise().ConcatenateBuffers();
		}

	}
}

