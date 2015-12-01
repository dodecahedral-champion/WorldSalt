namespace WorldSalt.UnitTests.Network.Streams.Frames {
    using NUnit.Framework;
    using Rhino.Mocks;
    using WorldSalt.Network.Direction;
    using WorldSalt.Network.Frames;
    using WorldSalt.Network.Streams.Bytes;
    using WorldSalt.Network.Streams.Frames;

    [TestFixture]
	public class FrameSinkFactoryUnitTest {
		private IByteSink<FromServer> byteSink;
		private ITypedFrame<FromServer> frame;

		private FrameSinkFactory<FromServer> target;

		[SetUp]
		public void Setup() {
			byteSink = MockRepository.GenerateMock<IByteSink<FromServer>>();
			frame = MockRepository.GenerateMock<ITypedFrame<FromServer>>();

			target = new FrameSinkFactory<FromServer>();
		}

		[Test]
		public void ShouldRenderToByteSinkOnPut() {
			var expectedBytes = new byte[] {
				0x12, 0x34
			};
			byteSink.Expect(x => x.Put(expectedBytes));
			frame.Stub(x => x.GetBytes()).Return(expectedBytes);

			var sink = target.Create(byteSink);
			sink.Put(frame);

			byteSink.VerifyAllExpectations();
		}
	}
}
