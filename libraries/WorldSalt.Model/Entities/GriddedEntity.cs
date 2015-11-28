namespace WorldSalt.Model.Entities {
	using System;
	using WorldSalt.Model.Grids;
	using WorldSalt.Model.Values;

	public class GriddedEntity : IGriddedEntity {
		public IGrid Grid { get; private set; }

		public Guid Id { get; private set; }

		public IEntity Parent { get; private set; }

		public Placement Placement { get; private set; }

		public Velocity Velocity { get; private set; }

		public GriddedEntity(IGridFactory gridFactory, IEntity parent, Placement placement, Velocity velocity) {
			Grid = gridFactory.Create();
			Id = Guid.NewGuid();
			Parent = parent;
			Placement = placement;
			Velocity = velocity;
		}

		public GriddedEntity(IGridFactory gridFactory, Placement placement, Velocity velocity) : this(gridFactory, null, placement, velocity) {
		}

		public GriddedEntity(IGridFactory gridFactory, Placement placement) : this(gridFactory, placement, default(Velocity)) {
		}
	}
}
