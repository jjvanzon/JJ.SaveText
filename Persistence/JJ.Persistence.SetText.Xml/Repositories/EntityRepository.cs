using JJ.Framework.Persistence;
using JJ.Framework.Persistence.Xml;
using JJ.Framework.Reflection.Exceptions;
using JJ.Persistence.SetText.DefaultRepositories.RepositoryInterfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;

namespace JJ.Persistence.SetText.Xml.Repositories
{
    public class EntityRepository : IEntityRepository
    {
        private XmlContext _context;
        private XmlDocument _document;
        private XmlToEntityConverter _converter;

        public EntityRepository(IContext context)
        {
            if (context == null) throw new NullException(() => context);

            _context = (XmlContext)context;
            _document = _context.GetDocument<Entity>();
            _converter = _context.GetConverter();
        }

        public Entity Get(int id)
        {
            return _context.Get<Entity>(id);
        }

        public Entity TryGet(int id)
        {
            return _context.TryGet<Entity>(id);
        }

        public Entity Create()
        {
            return _context.Create<Entity>();
        }

        public void Delete(Entity entity)
        {
            _context.Delete(entity);
        }

        public void Update(Entity entity)
        {
            _context.Update(entity);
        }

        public IEnumerable<Entity> Search(string text)
        {
            throw new NotImplementedException();
        }

        public void Commit()
        {
            _context.Commit();
        }
    }
}
