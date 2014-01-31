using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JJ.Models.SetText.Persistence.RepositoryInterfaces
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
