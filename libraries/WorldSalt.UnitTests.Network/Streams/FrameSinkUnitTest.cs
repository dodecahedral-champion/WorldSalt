namespace WorldSalt.UnitTests.Network.Streams {
	using System;
	using NUnit.Framework;
	using Rhino.Mocks;
	using WorldSalt.Network.Direction;
	using WorldSalt.Network.Frames;
	using WorldSalt.Network.Streams;
	using WorldSalt.Network.Streams.Bytes;

	[TestFixture]
	public class FrameSinkUnitTest {
		private FrameSink<FromClient> target;
		private IByteSink<FromClient> byteStream;

		[SetUp]
		public void Setup() {
			byteStream = MockRepository.GenerateMock<IByteSink<FromClient>>();
			target = new FrameSink<FromClient>(byteStream);
		}

		[Test]
		public void ShouldCloseUnderlying() {
			byteStream.Expect(x => x.Close());

			target.Close();

			byteStream.VerifyAllExpectations();
		}

		[Test]
		public void ShouldDisposeUnderlying() {
			byteStream.Expect(x => x.Dispose());

			target.Dispose();

			byteStream.VerifyAllExpectations();
		}

		[Test]
		public void ShouldPutToUnderlying() {
			var expectedBytes = new byte[] { 0x12, 0x34, 0x56, 0x78 };
			var frame = MockRepository.GenerateMock<ITypedFrame<FromClient>>();
			frame.Expect(x => x.GetBytes()).Return(expectedBytes);
			byteStream.Expect(x => x.Put(expectedBytes));

			target.Put(frame);

			byteStream.VerifyAllExpectations();
			frame.VerifyAllExpectations();
		}
	}
}
