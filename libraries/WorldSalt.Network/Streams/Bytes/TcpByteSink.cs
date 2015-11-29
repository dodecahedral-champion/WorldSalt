namespace WorldSalt.Network.Streams.Bytes {
	using System;
	using System.Net.Sockets;
	using WorldSalt.Network.Direction;

	public class TcpByteSink<TDir> : ByteConsumerStreamWrapper<TDir>, IStreamCloseable where TDir : IDirection {
		private TcpClient socket;

		public TcpByteSink(TcpClient socket) : base(socket.GetStream()) {
			this.socket = socket;
		}

		public void Dispose() {
			underlying.Close();
			underlying.Dispose();
		}

		public void Close() {
			socket.Close();
		}
	}
}
