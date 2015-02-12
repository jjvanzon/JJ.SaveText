using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace JJ.Persistence.SetText.DefaultRepositories.RepositoryInterfaces
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
