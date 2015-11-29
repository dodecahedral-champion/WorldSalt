namespace WorldSalt.Network.Streams {
	using System;
	using WorldSalt.Network.Direction;
	using WorldSalt.Network.Frames;
	using WorldSalt.Network.Payloads;
	using WorldSalt.Network.Streams.Bytes;
	using WorldSalt.Network.SerialisationExtensions;

	public class RawFrameSource<TDir> : IRawFrameSource<TDir> where TDir : IDirection {
		private IByteSource<TDir> underlying;
		private IPayloadFactory<TDir> payloadFactory;
		private IFrameFactory<TDir> frameFactory;

		public RawFrameSource(IByteSource<TDir> underlying, IPayloadFactory<TDir> payloadFactory, IFrameFactory<TDir> frameFactory) {
			this.underlying = underlying;
			this.payloadFactory = payloadFactory;
			this.frameFactory = frameFactory;
		}

		public void Dispose() {
			underlying.Dispose();
		}

		public void Close() {
			underlying.Close();
		}

		public IUntypedFrame<TDir> Take() {
			UInt32 length;
			Byte type;
			Byte subtype;
			Byte[] payloadBytes;
			underlying
				.Deserialise(out length)
					.Deserialise(out type)
					.Deserialise(out subtype)
					.Deserialise(out payloadBytes, length);
			var payload = payloadFactory.CreateUntypedPayload(payloadBytes);
			return frameFactory.CreateUntyped(type, subtype, payload);
		}
	}
}
