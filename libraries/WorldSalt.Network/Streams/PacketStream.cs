namespace WorldSalt.Network {
	using System;
	using System.Net.Sockets;
	using WorldSalt.Network.Direction;
	using WorldSalt.Network.Packets;
	using WorldSalt.Network.SerialisationExtensions;

	public class PacketStream<TConsumeDirection, TProduceDirection> : IStreamDuplex<ITypedPacket<TConsumeDirection>, ITypedPacket<TProduceDirection>> where TConsumeDirection : IDirection where TProduceDirection : IDirection {
		TcpClient socket;
		NetworkStream stream;
		IPacketFactory<TProduceDirection> packetFactory;
		IPayloadFactory<TProduceDirection> payloadFactory;

		public PacketStream(TcpClient socket, IPacketFactory<TProduceDirection> packetFactory, IPayloadFactory<TProduceDirection> payloadFactory) {
			this.socket = socket;
			this.stream = socket.GetStream();
			this.packetFactory = packetFactory;
			this.payloadFactory = payloadFactory;
		}

		public ITypedPacket<TProduceDirection> Take() {
			IUntypedPacket<TProduceDirection> rawPacket = TakeRaw();
			var typedPayload = payloadFactory.ConvertPayload(rawPacket.Payload, rawPacket.Type, rawPacket.Subtype);
			return packetFactory.Create(typedPayload);
		}

		public void Put(ITypedPacket<TConsumeDirection> value) {
			var wireFormat = value.GetBytes();
			stream.Write(wireFormat, 0, wireFormat.Length);
		}

		public void Close() {
			socket.Close();
		}

		private IUntypedPacket<TProduceDirection> TakeRaw() {
			UInt32 packetLength;
			Byte packetType;
			Byte packetSubtype;
			Byte[] payloadBytes;
			stream
				.Deserialise(out packetLength)
				.Deserialise(out packetType)
				.Deserialise(out packetSubtype)
				.Deserialise(out payloadBytes, packetLength);
			var payload = payloadFactory.CreateUntypedPayload(payloadBytes);
			return packetFactory.CreateUntyped(packetType, packetSubtype, payload);
		}
	}
}
