namespace WorldSalt.Network.Payloads {
	using System;
	using WorldSalt.Network.Direction;
	using WorldSalt.Network.Payloads.Connection;

	public class PayloadTypedCreatorFromClient : IPayloadTypedCreator<FromClient> {
		public ITypedPayload<FromClient> Create(Byte type, Byte subtype) {
			switch(type) {
				case 0x00:
					return CreateConnectionPayload(type, subtype);
				default:
					throw UnknownType(type, subtype);
			}
		}

		private ITypedPayload<FromClient> CreateConnectionPayload(Byte type, Byte subtype) {
			switch(subtype) {
				case 0x00:
					return new ConnectPayload();
				case 0x01:
					throw BadTypeForFlowDirection(type, subtype);
				case 0x02:
					throw BadTypeForFlowDirection(type, subtype);
				case 0x03:
					return new DisconnectPayload();
				case 0x04:
					throw BadTypeForFlowDirection(type, subtype);
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
