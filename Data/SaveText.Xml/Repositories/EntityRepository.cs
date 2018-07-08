using System;
using System.Collections.Generic;
using System.Xml;
using JJ.Data.SaveText.DefaultRepositories.RepositoryInterfaces;
using JJ.Framework.Data;
using JJ.Framework.Data.Xml;
using JJ.Framework.Exceptions.Basic;
using JetBrains.Annotations;

// ReSharper disable NotAccessedField.Local

namespace JJ.Data.SaveText.Xml.Repositories
{
    public class EntityRepository : IEntityRepository
    {
        private readonly XmlContext _context;
        private XmlDocument _document;
        private XmlToEntityConverter _converter;

        [UsedImplicitly]
        public EntityRepository(IContext context)
        {
            _context = (XmlContext)context ?? throw new NullException(() => context);
            _document = _context.GetDocument<Entity>();
            _converter = _context.GetConverter();
        }

        public Entity Get(int id) => _context.Get<Entity>(id);
        public Entity TryGet(int id) => _context.TryGet<Entity>(id);
        public Entity Create() => _context.Create<Entity>();
        public void Delete(Entity entity) => _context.Delete(entity);
        public void Update(Entity entity) => _context.Update(entity);
        public IEnumerable<Entity> Search(string text) => throw new NotImplementedException();
        public void Commit() => _context.Commit();
    }
}