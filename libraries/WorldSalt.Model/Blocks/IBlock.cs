namespace WorldSalt.Model.Blocks {
	using System;
	using WorldSalt.Model.Values;

	public interface IBlock {
		Guid Type { get; }
		Byte SubType { get; }
		BlockOrientation Orientation { get; }
		IBlockState State { get; }
	}
}
