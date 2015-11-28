namespace WorldSalt.Model {
	using WorldSalt.Model.Blocks;
	using WorldSalt.Model.Values;

	public interface IGrid {
		GridBounds Bounds { get; }
		IBlock GetBlock(GridAddress address);
		void SetBlock(GridAddress address, IBlock block);
	}
}
