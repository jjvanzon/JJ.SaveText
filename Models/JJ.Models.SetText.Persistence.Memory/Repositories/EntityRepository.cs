﻿using JJ.Framework.Persistence;
using JJ.Framework.Persistence.Memory;
using JJ.Models.SetText.Persistence.RepositoryInterfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace JJ.Models.SetText.Persistence.Memory.Repositories
{
    public class EntityRepository : IEntityRepository
    {
        private MemoryContext _context;

        public EntityRepository(IContext context)
        {
            if (context == null) throw new ArgumentNullException("context");

            _context = (MemoryContext)context;
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