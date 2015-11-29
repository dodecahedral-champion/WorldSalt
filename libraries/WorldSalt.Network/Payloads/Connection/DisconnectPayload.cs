namespace WorldSalt.Network.Payloads.Connection {
	using System.IO;
	using WorldSalt.Network.Direction;
	using WorldSalt.Network.SerialisationExtensions;

	public class DisconnectPayload : BaseTypedPayload<FromClient> {
		public override byte Type { get { return 0x00; } }
		public override byte Subtype { get { return 0x03; } }

		public override uint Length {
			get { return 0; }
		}

		public DisconnectPayload() {
		}

		public override void SetBytes(byte[] bytes) {
			MakeDirectedByteStream(bytes).AssertEnd();
		}

		public override byte[] GetBytes() {
			return new byte[0];
		}
	}
}
