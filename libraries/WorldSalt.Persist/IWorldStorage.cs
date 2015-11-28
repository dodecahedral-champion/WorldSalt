namespace WorldSalt.Persist {
	using WorldSalt.Model;

	public interface IWorldStorage {
		IWorld Load();
		void Save(IWorld world);
	}
}
