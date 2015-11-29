namespace WorldSalt.Network.Packets.Connection {
	using System;
	using System.Collections.Generic;
	using System.Linq;
	using WorldSalt.Network.Direction;
	using WorldSalt.Network.Serialisation;

	public class ConnectPayload : ITypedPayload<FromClient> {
		public byte Type { get { return 0x00; } }
		public byte Subtype { get { return 0x00; } }

		public uint Length {
			get {
				throw new NotImplementedException();
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

		public void SetBytes(byte[] bytes) {
			string username;
			UInt64 preferredProtocol;
			IList<UInt64> supportedProtocols;

			UInt64 index = 0;
			index = SerialisationHelper.LoadString(bytes, index, out username);
			index = SerialisationHelper.LoadU64(bytes, index, out preferredProtocol);
			index = SerialisationHelper.LoadList(bytes, index, out supportedProtocols, SerialisationHelper.LoadU64);
			SerialisationHelper.AssertEnd(bytes, index);

			Username = username;
			PreferredProtocol = preferredProtocol;
			SupportedProtocols = supportedProtocols;
		}

		public byte[] GetBytes() {
			return Username.Serialise()
				.Concat(PreferredProtocol.Serialise())
				.Concat(SupportedProtocols.Serialise(SerialisationHelper.Serialise))
				.ConcatenateBuffers();
		}
	}
}
