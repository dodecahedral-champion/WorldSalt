namespace WorldSalt.Model {
	using System.Collections.Generic;
	using WorldSalt.Model.Entities;

	public interface IWorld {
		IEnumerable<IEntity> Entities { get; }
	}
}
