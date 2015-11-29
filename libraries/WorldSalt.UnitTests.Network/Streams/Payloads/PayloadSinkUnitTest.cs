namespace WorldSalt.UnitTests.Network.Streams.Payloads {
	using System;
	using NUnit.Framework;
	using Rhino.Mocks;
	using WorldSalt.Network.Direction;
	using WorldSalt.Network.Frames;
	using WorldSalt.Network.Payloads;
	using WorldSalt.Network.Streams;
	using WorldSalt.Network.Streams.Payloads;

	[TestFixture]
	public class PayloadSinkUnitTest {
		private PayloadSink<FromClient> target;
		private IStreamConsumer<ITypedFrame<FromClient>> frameStream;
		private IFrameFactory<FromClient> frameFactory;

		[SetUp]
		public void Setup() {
			frameStream = MockRepository.GenerateMock<IStreamConsumer<ITypedFrame<FromClient>>>();
			frameFactory = MockRepository.GenerateMock<IFrameFactory<FromClient>>();

			target = new PayloadSink<FromClient>(frameStream, frameFactory);
		}

		[Test]
		public void ShouldCloseUnderlying() {
			frameStream.Expect(x => x.Close());

			target.Close();

			frameStream.VerifyAllExpectations();
		}

		[Test]
		public void ShouldDisposeUnderlying() {
			frameStream.Expect(x => x.Dispose());

			target.Dispose();

			frameStream.VerifyAllExpectations();
		}

		[Test]
		public void ShouldCreateFrameForUnderlying() {
			var payload = MockRepository.GenerateMock<ITypedPayload<FromClient>>();
			var frame = MockRepository.GenerateMock<ITypedFrame<ITypedPayload<FromClient>, FromClient>>();
			frameFactory.Expect(x => x.Create(payload)).Return(frame);
			frameStream.Expect(x => x.Put(frame));

			target.Put(payload);

			frameStream.VerifyAllExpectations();
		}
	}
}
