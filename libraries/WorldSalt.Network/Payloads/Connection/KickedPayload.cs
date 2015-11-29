namespace WorldSalt.Network.Payloads.Connection {
	using System;
	using System.IO;
	using WorldSalt.Network.Direction;
	using WorldSalt.Network.SerialisationExtensions;

	public class KickedPayload : ITypedPayload<FromServer> {
		public byte Type { get { return 0x00; } }
		public byte Subtype { get { return 0x04; } }

		public uint Length {
			get {
				return
					Message.SerialisationLength();
			}
		}

		public string Message { get; private set; }

		public KickedPayload() : this("") {
		}

		public KickedPayload(string message) {
			Message = message;
		}

		public void SetBytes(byte[] bytes) {
			string message;

			new MemoryStream(bytes)
				.Deserialise(out message)
				.AssertEnd();

			Message = message;
		}

		public byte[] GetBytes() {
			return Message.Serialise()
				.ConcatenateBuffers();
		}
	}
}
