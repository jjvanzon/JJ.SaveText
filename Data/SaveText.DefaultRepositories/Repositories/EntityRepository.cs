using JetBrains.Annotations;
using JJ.Data.SaveText.DefaultRepositories.RepositoryInterfaces;
using JJ.Framework.Data;
using JJ.Framework.Exceptions.Basic;

namespace JJ.Data.SaveText.DefaultRepositories.Repositories
{
    [UsedImplicitly]
    public class EntityRepository : IEntityRepository
    {
        private readonly IContext _context;

        public EntityRepository(IContext context) => _context = context ?? throw new NullException(() => context);

        public Entity Get(int id) => _context.Get<Entity>(id);
        public Entity TryGet(int id) => _context.TryGet<Entity>(id);
        public Entity Create() => _context.Create<Entity>();
        public void Delete(Entity entity) => _context.Delete(entity);
        public void Update(Entity entity) => _context.Update(entity);
        public void Commit() => _context.Commit();
    }
}