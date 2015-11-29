namespace WorldSalt.Network.Packets {
	using System;
	using WorldSalt.Network.Direction;
	using WorldSalt.Network.Packets.Connection;

	public class PayloadTypedCreatorFromServer : IPayloadTypedCreator<FromServer> {
		public ITypedPayload<FromServer> Create(Byte packetType, Byte packetSubtype) {
			switch(packetType) {
				case 0x00:
					return CreateConnectionPayload(packetType, packetSubtype);
				default:
					throw new NotSupportedException(string.Format("unknown packet type {0} subtype {1}", packetType, packetSubtype));
			}
		}

		private ITypedPayload<FromServer> CreateConnectionPayload(byte packetType, byte packetSubtype) {
			switch(packetSubtype) {
				case 0x00:
					throw BadPacketForDirection(packetType, packetSubtype);
				case 0x01:
					return new ConnectedPayload();
				case 0x02:
					return new UnsupportedProtocolVersionPayload();
				case 0x03:
					throw BadPacketForDirection(packetType, packetSubtype);
				case 0x04:
					return new KickedPayload();
				default:
					throw UnknownPacket(packetType, packetSubtype);
			}
		}

		private static Exception BadPacketForDirection(byte packetType, byte packetSubtype) {
			return new InvalidOperationException(string.Format("bad packet for this direction: type {0} subtype {1}", packetType, packetSubtype));
		}

		static Exception UnknownPacket(byte packetType, byte packetSubtype) {
			return new NotSupportedException(string.Format("unknown packet type {0} subtype {1}", packetType, packetSubtype));
		}
	}
}

