namespace WorldSalt.Network.Streams.Bytes {
	using System.Net.Sockets;
	using WorldSalt.Network.Direction;

	public class TcpByteSink<TDir> : ByteConsumerStreamWrapper<TDir>, IStreamCloseable where TDir : IDirection {
		private readonly TcpClient socket;

		public TcpByteSink(TcpClient socket) : base(socket.GetStream()) {
			this.socket = socket;
		}

		public new void Close() {
			socket.Close();
			base.Close();
		}
	}
}
