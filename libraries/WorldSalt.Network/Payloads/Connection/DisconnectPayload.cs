namespace WorldSalt.Network.Payloads.Connection {
	using System.IO;
	using WorldSalt.Network.Direction;
	using WorldSalt.Network.SerialisationExtensions;

	public class DisconnectPayload : ITypedPayload<FromClient> {
		public byte Type { get { return 0x00; } }
		public byte Subtype { get { return 0x03; } }

		public uint Length {
			get { return 0; }
		}

		public DisconnectPayload() {
		}

		public void SetBytes(byte[] bytes) {
			new MemoryStream(bytes).AssertEnd();
		}

		public byte[] GetBytes() {
			return new byte[0];
		}
	}
}
