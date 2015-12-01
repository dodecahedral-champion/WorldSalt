namespace WorldSalt.Network.Streams.Bytes {
	using System.Net.Sockets;
	using WorldSalt.Network.Direction;

	public class TcpByteSource<TDir> : ByteProducerStreamWrapper<TDir>, IStreamCloseable where TDir : IDirection {
		private readonly TcpClient socket;

		public TcpByteSource(TcpClient socket) : base(socket.GetStream()) {
			this.socket = socket;
		}

		public new void Close() {
			socket.Close();
			base.Close();
		}
	}
}
