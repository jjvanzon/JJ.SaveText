namespace JJ.Data.SaveText.DefaultRepositories.RepositoryInterfaces
{
	public interface IEntityRepository
	{
		Entity TryGet(int id);
		Entity Get(int id);
		Entity Create();
		void Delete(Entity entity);
		void Update(Entity entity);
		void Commit();
	}
}
