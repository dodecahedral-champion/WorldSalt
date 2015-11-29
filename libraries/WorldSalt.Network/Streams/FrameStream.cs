namespace WorldSalt.Network.Streams {
	using System;
	using System.Net.Sockets;
	using WorldSalt.Network.Direction;
	using WorldSalt.Network.Frames;
	using WorldSalt.Network.Payloads;
	using WorldSalt.Network.SerialisationExtensions;
	using WorldSalt.Network.Streams.Bytes;

	public class FrameStream<TConsumeDirection, TProduceDirection> : IStreamDuplex<ITypedFrame<TConsumeDirection>, ITypedFrame<TProduceDirection>> where TConsumeDirection : IDirection where TProduceDirection : IDirection {
		private IByteSource<TProduceDirection> byteSource;
		private IByteSink<TConsumeDirection> byteSink;
		private IFrameFactory<TProduceDirection> frameFactory;
		private IPayloadFactory<TProduceDirection> payloadFactory;

		public FrameStream(IByteSource<TProduceDirection> byteSource, IByteSink<TConsumeDirection> byteSink, IFrameFactory<TProduceDirection> frameFactory, IPayloadFactory<TProduceDirection> payloadFactory) {
			this.byteSource = byteSource;
			this.byteSink = byteSink;
			this.frameFactory = frameFactory;
			this.payloadFactory = payloadFactory;
		}

		public void Dispose() {
			Close();
			byteSource.Dispose();
			byteSink.Dispose();
		}

		public ITypedFrame<TProduceDirection> Take() {
			IUntypedFrame<TProduceDirection> rawFrame = TakeRaw();
			var typedPayload = payloadFactory.ConvertPayload(rawFrame.Payload, rawFrame.Type, rawFrame.Subtype);
			return frameFactory.Create(typedPayload);
		}

		public void Put(ITypedFrame<TConsumeDirection> value) {
			byteSink.Put(value.GetBytes());
		}

		public void Close() {
			byteSource.Close();
			byteSink.Close();
		}

		private IUntypedFrame<TProduceDirection> TakeRaw() {
			UInt32 length;
			Byte type;
			Byte subtype;
			Byte[] payloadBytes;
			byteSource
				.Deserialise(out length)
				.Deserialise(out type)
				.Deserialise(out subtype)
				.Deserialise(out payloadBytes, length);
			var payload = payloadFactory.CreateUntypedPayload(payloadBytes);
			return frameFactory.CreateUntyped(type, subtype, payload);
		}
	}
}
