namespace WorldSalt.UnitTests.Network {
	using System;
	using System.Collections.Generic;
	using System.IO;
	using System.Linq;
	using System.Net;
	using System.Net.Sockets;
	using NUnit.Framework;
	using Rhino.Mocks;
	using WorldSalt.Network.Direction;
	using WorldSalt.Network.Streams.Bytes;

	[TestFixture]
	public class TcpStreamUnitTest {
		const int VERY_LONG_TIMEOUT = 2000;
		const int MAX_READS = 1024;
		const int TEST_RENDEZVOUS_PORT = 2227;
		private TcpClient remoteSocket;
		private Stream remoteStream;
		private TcpListener listener;
		private TcpClient streamSocket;

		private TcpStream<FromServer, FromClient> target;

		[SetUp]
		public void Setup() {
			listener = new TcpListener(IPAddress.Any, TEST_RENDEZVOUS_PORT);
			listener.Start();
			remoteSocket = new TcpClient("localhost", TEST_RENDEZVOUS_PORT);
			streamSocket = listener.AcceptTcpClient();
			target = new TcpStream<FromServer, FromClient>(streamSocket);
			remoteStream = remoteSocket.GetStream();
			remoteSocket.ReceiveTimeout = VERY_LONG_TIMEOUT;
			remoteStream.ReadTimeout = 1;
		}

		[TearDown]
		public void Teardown() {
			remoteSocket.Close();
			streamSocket.Close();
			listener.Stop();
		}

		[Test]
		public void ShouldCloseSocketWhenStreamClosed() {
			Assert.IsTrue(remoteSocket.Connected);

			target.Close();

			Assert.AreEqual(-1, remoteStream.ReadByte());
		}

		[Test]
		public void ShouldPutSingleByte() {
			var expectedByte = (byte)0x34;

			target.Put(expectedByte);

			Assert.AreEqual(expectedByte, remoteStream.ReadByte());
		}

		[Test]
		public void ShouldPutBytes() {
			var expectedBytes = new byte[] { 0x12, 0x34, 0x56, 0x78 };

			target.Put(expectedBytes);

			CollectionAssert.AreEqual(expectedBytes, ReadBytesInstant(MAX_READS));
		}

		[Test]
		public void ShouldReadSingleByte() {
			var expectedByte = (byte)0x34;
			remoteStream.WriteByte(expectedByte);

			var actualByte = target.Read();

			Assert.AreEqual(expectedByte, actualByte);
		}

		[Test]
		public void ShouldReadSequence() {
			var expectedBytes = new byte[] { 0x12, 0x34, 0x56, 0x78 };
			WriteSequence(expectedBytes);

			var actualBytes = target.ReadSequence(4);

			CollectionAssert.AreEqual(expectedBytes, actualBytes);
		}

		[Test]
		public void ShouldReadPartialSequences() {
			var expectedBytes1 = new byte[] { 0x12, 0x34, 0x56, 0x78 };
			var expectedBytes2 = new byte[] { 0x9a, 0xbc, 0xde, 0xff };
			WriteSequence(new byte[] { 0x12, 0x34, 0x56, 0x78, 0x9a, 0xbc, 0xde, 0xff });

			var actualBytes1 = target.ReadSequence(4);
			var actualBytes2 = target.ReadSequence(4);

			CollectionAssert.AreEqual(expectedBytes1, actualBytes1);
			CollectionAssert.AreEqual(expectedBytes2, actualBytes2);
		}

		[Test]
		public void ShouldTryReadSequence() {
			var expectedBytes = new byte[] { 0x12, 0x34, 0x56, 0x78 };
			WriteSequence(expectedBytes);

			Byte[] actualBytes;
			var actualNumRead = target.TryReadSequence(6, out actualBytes);
			var actualBytesRead = actualBytes.Take((int)actualNumRead).ToArray();

			Assert.AreEqual(4, actualNumRead);
			Assert.AreEqual(6, actualBytes.Length);
			CollectionAssert.AreEqual(expectedBytes, actualBytesRead);
		}

		[Test]
		public void ShouldThrowIfReadingSequenceWhenStreamTooShort() {
			WriteSequence(new byte[] { 0x12, 0x34, 0x56, 0x78 });

			Assert.Throws<IOException>(() => target.ReadSequence(8));
		}

		[Test]
		public void ShouldThrowIfReadingSingleByteFromClosedStream() {
			target.Close();

			Assert.Throws<ObjectDisposedException>(() => target.Read());
		}

		[Test]
		public void ShouldThrowIfReadingSequenceFromClosedStream() {
			target.Close();

			Assert.Throws<ObjectDisposedException>(() => target.ReadSequence(1));
		}

		[Test]
		public void ShouldThrowIfTryingToReadSequenceFromClosedStream() {
			target.Close();
			byte[] sequence;

			Assert.Throws<ObjectDisposedException>(() => target.TryReadSequence(1, out sequence));
		}

		[Test]
		public void ShouldNotThrowIfWritingSingleByteToClosedStream() {
			target.Close();

			Assert.DoesNotThrow(() => remoteStream.WriteByte(0x12));
		}

		[Test]
		public void ShouldNotThrowIfWritingSequenceToClosedStream() {
			target.Close();

			Assert.DoesNotThrow(() => WriteSequence(new byte[] { 0x12, 0x34, 0x56, 0x78 }));
		}

		private Nullable<Byte> ReadInstant() {
			var buffer = new byte[1];
			try {
				var numRead = remoteStream.Read(buffer, 0, 1);
				if(numRead == 0) {
					return null;
				}
			} catch(IOException) {
				return null;
			}

			return buffer[0];
		}

		private Byte[] ReadBytesInstant(int maxBytes) {
			return Enumerable.Repeat<Func<Nullable<Byte>>>(ReadInstant, MAX_READS).Select(f => f())
				.TakeWhile(x => x.HasValue).Select(x => x.Value)
				.ToArray();
		}

		private void WriteSequence(Byte[] sequence) {
			remoteStream.Write(sequence, 0, sequence.Length);
		}
	}
}
