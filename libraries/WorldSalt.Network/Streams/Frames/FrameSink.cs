namespace WorldSalt.Network.Streams.Frames {
	using WorldSalt.Network.Direction;
	using WorldSalt.Network.Frames;
	using WorldSalt.Network.Streams.Bytes;

	public class FrameSink<TDir> : IStreamConsumer<ITypedFrame<TDir>> where TDir : IDirection {
		private IByteSink<TDir> underlying;

		public FrameSink(IByteSink<TDir> underlying) {
			this.underlying = underlying;
		}

		public void Dispose() {
			underlying.Dispose();
		}

		public void Close() {
			underlying.Close();
		}

		public void Put(ITypedFrame<TDir> value) {
			underlying.Put(value.GetBytes());
		}
	}
}
