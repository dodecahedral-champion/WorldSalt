namespace WorldSalt.Network.Streams.Bytes {
	using System;
	using System.IO;
	using System.Net.Sockets;
	using WorldSalt.Network.Direction;

	public class TcpStream<TConsumeDir, TProduceDir> : IStreamCloseable, IByteStreamConsumer<TConsumeDir>, IByteStreamProducer<TProduceDir> where TConsumeDir : IDirection where TProduceDir : IDirection {
		private TcpClient socket;
		private NetworkStream stream;

		public TcpStream(TcpClient socket) {
			this.socket = socket;
			socket.NoDelay = true;
			this.stream = socket.GetStream();
		}

		public void Dispose() {
			Close();
			stream.Dispose();
		}

		public void Close() {
			socket.Close();
		}

		public void Put(byte item) {
			stream.WriteByte(item);
		}

		public void Put(byte[] bytes) {
			stream.Write(bytes, 0, bytes.Length);
		}

		public byte Read() {
			var value = stream.ReadByte();
			if(value < 0) {
				throw new IOException("unable to read byte from TCP stream");
			}

			return (byte)value;
		}

		public byte[] ReadSequence(uint desiredLength) {
			byte[] sequence;
			var actualReads = TryReadSequence(desiredLength, out sequence);
			if(actualReads != desiredLength) {
				throw new IOException(string.Format("unable to read {0} bytes; only {1} were available", desiredLength, actualReads));
			}

			return sequence;
		}

		public UInt32 TryReadSequence(UInt32 desiredLength, out byte[] sequence) {
			var smallLength = (int)desiredLength;
			var destination = new byte[smallLength];
			var actualReads = stream.Read(destination, 0, smallLength);
			sequence = destination;
			return (UInt32)actualReads;
		}
	}
}
