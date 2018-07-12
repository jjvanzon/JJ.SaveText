﻿using JJ.Data.SaveText.DefaultRepositories.RepositoryInterfaces;
using JJ.Framework.Data;
using JJ.Framework.Exceptions;
using JJ.Framework.Exceptions.Basic;

namespace JJ.Data.SaveText.DefaultRepositories.Repositories
{
	public class EntityRepository : IEntityRepository
	{
		private IContext _context;

		public EntityRepository(IContext context)
		{
			if (context == null) throw new NullException(() => context);

			_context = context;
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

		public void Commit()
		{
			_context.Commit();
		}
	}
}
