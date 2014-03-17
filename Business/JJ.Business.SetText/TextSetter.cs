using JJ.Models.SetText;
using JJ.Models.SetText.Persistence.RepositoryInterfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace JJ.Business.SetText
{
    public class TextSetter
    {
        private const int ENTITY_ID = 1;

        private IEntityRepository _entityRepository;

        public TextSetter(IEntityRepository entityRepository)
        {
            if (entityRepository == null) throw new ArgumentNullException("entityRepository");
            _entityRepository = entityRepository;
        }

        public string GetText()
        {
            Entity entity = GetEntity();
            return entity.Text;
        }

        public void SetText(string value)
        {
            Entity entity = GetEntity();
            entity.Text = value;
            _entityRepository.Update(entity);
        }

        private Entity GetEntity()
        {
            Entity entity = _entityRepository.TryGet(ENTITY_ID);
            if (entity == null)
            {
                entity = _entityRepository.Create();
                entity.ID = ENTITY_ID;
                _entityRepository.Update(entity);
            }
            return entity;
        }
    }
}
