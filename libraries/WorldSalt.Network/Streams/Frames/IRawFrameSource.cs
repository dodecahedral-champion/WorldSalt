namespace WorldSalt.Network.Streams.Frames {
	using System;
	using WorldSalt.Network.Streams;
	using WorldSalt.Network.Direction;
	using WorldSalt.Network.Frames;

	public interface IRawFrameSource<TDir> : IStreamProducer<IUntypedFrame<TDir>> where TDir : IDirection {
	}
}
