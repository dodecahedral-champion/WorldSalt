namespace WorldSalt.Model.Entities {
	using System;
	using WorldSalt.Model.Values;

	public interface IEntity {
		Guid Id { get; }
		IEntity Parent { get; }
		Placement Placement { get; }
		Velocity Velocity { get; }
	}
}
