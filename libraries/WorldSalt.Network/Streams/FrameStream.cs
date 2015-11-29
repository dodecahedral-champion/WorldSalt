namespace WorldSalt.Network.Streams {
	using System;
	using System.Net.Sockets;
	using WorldSalt.Network.Direction;
	using WorldSalt.Network.Frames;
	using WorldSalt.Network.Payloads;
	using WorldSalt.Network.SerialisationExtensions;

	public class FrameStream<TConsumeDirection, TProduceDirection> : IStreamDuplex<ITypedFrame<TConsumeDirection>, ITypedFrame<TProduceDirection>> where TConsumeDirection : IDirection where TProduceDirection : IDirection {
		TcpClient socket;
		NetworkStream stream;
		IFrameFactory<TProduceDirection> frameFactory;
		IPayloadFactory<TProduceDirection> payloadFactory;

		public FrameStream(TcpClient socket, IFrameFactory<TProduceDirection> frameFactory, IPayloadFactory<TProduceDirection> payloadFactory) {
			this.socket = socket;
			this.stream = socket.GetStream();
			this.frameFactory = frameFactory;
			this.payloadFactory = payloadFactory;
		}

		public void Dispose() {
			Close();
			stream.Dispose();
		}

		public ITypedFrame<TProduceDirection> Take() {
			IUntypedFrame<TProduceDirection> rawFrame = TakeRaw();
			var typedPayload = payloadFactory.ConvertPayload(rawFrame.Payload, rawFrame.Type, rawFrame.Subtype);
			return frameFactory.Create(typedPayload);
		}

		public void Put(ITypedFrame<TConsumeDirection> value) {
			var wireFormat = value.GetBytes();
			stream.Write(wireFormat, 0, wireFormat.Length);
		}

		public void Close() {
			socket.Close();
		}

		private IUntypedFrame<TProduceDirection> TakeRaw() {
			UInt32 length;
			Byte type;
			Byte subtype;
			Byte[] payloadBytes;
			stream
				.Deserialise(out length)
				.Deserialise(out type)
				.Deserialise(out subtype)
				.Deserialise(out payloadBytes, length);
			var payload = payloadFactory.CreateUntypedPayload(payloadBytes);
			return frameFactory.CreateUntyped(type, subtype, payload);
		}
	}
}
