namespace WorldSalt.Model.Blocks {
	using System;
	using WorldSalt.Model.Values;

	public class Block : IBlock {
		public Guid Type { get; private set; }
		public Byte SubType { get; private set; }
		public BlockOrientation Orientation { get; private set; }
		public IBlockState State { get; private set; }

		public Block(Guid type, Byte subtype, BlockOrientation orientation, IBlockState state) {
			Type = type;
			SubType = subtype;
			Orientation = orientation;
			State = state;
		}

		public Block(Guid type, Byte subtype) : this(type, subtype, default(BlockOrientation), null) {
		}

		public Block(Guid type) : this(type, 0x00) {
		}
	}
}
