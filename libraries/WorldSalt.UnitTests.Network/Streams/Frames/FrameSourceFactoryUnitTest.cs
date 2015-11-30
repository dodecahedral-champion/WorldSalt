namespace WorldSalt.UnitTests.Network {
	using System;
	using System.Net.Sockets;
	using NUnit.Framework;
	using Rhino.Mocks;
	using WorldSalt.Network.Direction;
	using WorldSalt.Network.Frames;
	using WorldSalt.Network.Payloads;
	using WorldSalt.Network.Payloads.Connection;
	using WorldSalt.Network.Streams.Bytes;
	using WorldSalt.Network.Streams.Frames;

	[TestFixture]
	public class FrameSourceFactoryUnitTest {
		private IByteSource<FromServer> byteSource;
		private IPayloadFactory<FromServer> payloadFactory;
		private IFrameFactory<FromServer> frameFactory;

		private FrameSourceFactory<FromServer> target;

		[SetUp]
		public void Setup() {
			byteSource = MockRepository.GenerateMock<IByteSource<FromServer>>();
			payloadFactory = MockRepository.GenerateMock<IPayloadFactory<FromServer>>();
			frameFactory = MockRepository.GenerateMock<IFrameFactory<FromServer>>();

			target = new FrameSourceFactory<FromServer>(payloadFactory, frameFactory);
		}

		[Test]
		public void ShouldCallByteSourceOnTake() {
			var payloadBytes = new byte[] {
				0x03, 0x00, 0x00, 0x00,
				0x66, 0x6f, 0x6f
			};
			byteSource.Expect(x => x.ReadSequence(4)).Return(new byte[] { 0x07, 0x00, 0x00, 0x00 }).Repeat.Once();
			byteSource.Expect(x => x.Read()).Return(0x00).Repeat.Once();
			byteSource.Expect(x => x.Read()).Return(0x04).Repeat.Once();
			byteSource.Expect(x => x.ReadSequence(7)).Return(payloadBytes).Repeat.Once();
			var rawPayload = MockRepository.GenerateMock<IRawPayload<FromServer>>();
			var rawFrame = MockRepository.GenerateMock<IUntypedFrame<FromServer>>();
			var typedPayload = MockRepository.GenerateMock<ITypedPayload<FromServer>>();
			var typedFrame = MockRepository.GenerateMock<ITypedFrame<ITypedPayload<FromServer>, FromServer>>();
			rawFrame.Stub(x => x.Payload).Return(rawPayload);
			rawFrame.Stub(x => x.Type).Return(0xfe);
			rawFrame.Stub(x => x.Subtype).Return(0xff);
			payloadFactory.Expect(x => x.CreateUntypedPayload(payloadBytes)).Return(rawPayload);
			frameFactory.Expect(x => x.CreateUntyped(0x00, 0x04, rawPayload)).Return(rawFrame);
			payloadFactory.Expect(x => x.ConvertPayload(rawPayload, 0xfe, 0xff)).Return(typedPayload);
			frameFactory.Expect(x => x.Create(typedPayload)).Return(typedFrame);

			var source = target.Create(byteSource);
			var actualFrame = source.Take();

			Assert.AreSame(typedFrame, actualFrame);
			byteSource.VerifyAllExpectations();
			payloadFactory.VerifyAllExpectations();
			frameFactory.VerifyAllExpectations();
		}
	}
}
