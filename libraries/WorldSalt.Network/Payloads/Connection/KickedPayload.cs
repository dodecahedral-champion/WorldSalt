namespace WorldSalt.Network.Payloads.Connection {
	using System;
	using WorldSalt.Network.Direction;
	using WorldSalt.Network.SerialisationExtensions;

	public class KickedPayload : BaseTypedPayload<FromServer> {
		public override byte Type { get { return 0x00; } }
		public override byte Subtype { get { return 0x04; } }

		public override uint Length {
			get { return Message.SerialisationLength(); }
		}

		public string Message { get; private set; }

		public KickedPayload() : this("") {
		}

		public KickedPayload(string message) {
			Message = message;
		}

		public override void SetBytes(byte[] bytes) {
			string message;

			MakeDirectedByteStream(bytes)
				.Deserialise(out message)
				.AssertEnd();

			Message = message;
		}

		public override byte[] GetBytes() {
			return Message.Serialise()
				.ConcatenateBuffers();
		}
	}
}
