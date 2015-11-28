namespace WorldSalt.Model.Entities {
	using WorldSalt.Model.Grids;

	public interface IGriddedEntity : IEntity {
		IGrid Grid { get; }
	}
}
