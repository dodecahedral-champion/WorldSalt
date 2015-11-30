namespace WorldSalt.Network.Payloads.Connection {
	using System;
	using System.Collections.Generic;
	using System.Linq;
	using WorldSalt.Network.Direction;
	using WorldSalt.Network.SerialisationExtensions;

	public class ConnectPayload : BaseTypedPayload<FromClient> {
		public override byte Type { get { return 0x00; } }
		public override byte Subtype { get { return 0x00; } }

		public override uint Length {
			get {
				return
					Username.SerialisationLength()
					+ PreferredProtocol.SerialisationLength()
					+ SupportedProtocols.SerialisationLength(LengthCalculation.SerialisationLength);
			}
		}

		public string Username { get; private set; }
		public UInt64 PreferredProtocol { get; private set; }
		public IList<UInt64> SupportedProtocols { get; private set; }

		public ConnectPayload() : this("", ProtocolVersion.CURRENT, Enumerable.Empty<UInt64>()) {
		}

		public ConnectPayload(string username, UInt64 preferredProtocol, IEnumerable<UInt64> supportedProtocols) {
			Username = username;
			PreferredProtocol = preferredProtocol;
			SupportedProtocols = supportedProtocols.ToList();
		}

		public override void SetBytes(byte[] bytes) {
			string username;
			UInt64 preferredProtocol;
			IList<UInt64> supportedProtocols;

			MakeDirectedByteStream(bytes)
				.Deserialise(out username)
				.Deserialise(out preferredProtocol)
				.Deserialise(out supportedProtocols, Deserialisation.Deserialise)
				.AssertEnd();

			Username = username;
			PreferredProtocol = preferredProtocol;
			SupportedProtocols = supportedProtocols;
		}

		public override byte[] GetBytes() {
			return Username.Serialise()
				.Concat(PreferredProtocol.Serialise())
				.Concat(SupportedProtocols.Serialise(Serialisation.Serialise))
				.ConcatenateBuffers();
		}
	}
}
