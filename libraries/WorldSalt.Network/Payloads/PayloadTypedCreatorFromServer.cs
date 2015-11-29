namespace WorldSalt.Network.Payloads {
	using System;
	using WorldSalt.Network.Direction;
	using WorldSalt.Network.Payloads.Connection;

	public class PayloadTypedCreatorFromServer : IPayloadTypedCreator<FromServer> {
		public ITypedPayload<FromServer> Create(Byte type, Byte subtype) {
			switch(type) {
				case 0x00:
					return CreateConnectionPayload(type, subtype);
				default:
					throw UnknownType(type, subtype);
			}
		}

		private ITypedPayload<FromServer> CreateConnectionPayload(Byte type, Byte subtype) {
			switch(subtype) {
				case 0x00:
					throw BadTypeForFlowDirection(type, subtype);
				case 0x01:
					return new ConnectedPayload();
				case 0x02:
					return new UnsupportedProtocolVersionPayload();
				case 0x03:
					throw BadTypeForFlowDirection(type, subtype);
				case 0x04:
					return new KickedPayload();
				default:
					throw UnknownType(type, subtype);
			}
		}

		private static Exception BadTypeForFlowDirection(Byte type, Byte subtype) {
			return new InvalidOperationException(string.Format("bad type for this direction: type {0} subtype {1}", type, subtype));
		}

		static Exception UnknownType(Byte type, Byte subtype) {
			return new NotSupportedException(string.Format("unknown type {0} subtype {1}", type, subtype));
		}
	}
}

