namespace WorldSalt.Network.Payloads.Connection {
	using System;
	using System.Collections.Generic;
	using System.Linq;
	using WorldSalt.Network.Direction;
	using WorldSalt.Network.SerialisationExtensions;

	public class UnsupportedProtocolVersionPayload : BaseTypedPayload<FromServer> {
		public override byte Type { get { return 0x00; } }
		public override byte Subtype { get { return 0x02; } }

		public override uint Length {
			get {
				return
					PreferredProtocol.SerialisationLength()
					+ SupportedProtocols.SerialisationLength(LengthCalculation.SerialisationLength);
			}
		}

		public UInt64 PreferredProtocol { get; private set; }
		public IList<UInt64> SupportedProtocols { get; private set; }

		public UnsupportedProtocolVersionPayload() : this(ProtocolVersion.CURRENT, Enumerable.Empty<UInt64>()) {
		}

		public UnsupportedProtocolVersionPayload(UInt64 preferredProtocol, IEnumerable<UInt64> supportedProtocols) {
			PreferredProtocol = preferredProtocol;
			SupportedProtocols = supportedProtocols.ToList();
		}

		public override void SetBytes(byte[] bytes) {
			UInt64 preferredProtocol;
			IList<UInt64> supportedProtocols;

			MakeDirectedByteStream(bytes)
				.Deserialise(out preferredProtocol)
				.Deserialise(out supportedProtocols, Deserialisation.Deserialise)
				.AssertEnd();

			PreferredProtocol = preferredProtocol;
			SupportedProtocols = supportedProtocols;
		}

		public override byte[] GetBytes() {
			return PreferredProtocol.Serialise()
				.Concat(SupportedProtocols.Serialise(Serialisation.Serialise))
				.ConcatenateBuffers();
		}
	}
}
