namespace WorldSalt.Network.Streams.Payloads {
	using WorldSalt.Network.Direction;
	using WorldSalt.Network.Frames;
	using WorldSalt.Network.Payloads;

	public class PayloadSource<TDir> : IStreamProducer<ITypedPayload<TDir>> where TDir : IDirection {
		private IStreamProducer<ITypedFrame<TDir>> underlying;

		public PayloadSource(IStreamProducer<ITypedFrame<TDir>> underlying) {
			this.underlying = underlying;
		}

		public void Dispose() {
			underlying.Dispose();
		}

		public void Close() {
			underlying.Close();
		}

		public ITypedPayload<TDir> Take() {
			return underlying.Take().Payload;
		}
	}
}
