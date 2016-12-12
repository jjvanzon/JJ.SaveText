using JJ.Business.SaveText.Validation;
using JJ.Framework.Validation;
using JJ.Data.Canonical;
using JJ.Data.SaveText;
using JJ.Data.SaveText.DefaultRepositories.RepositoryInterfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using JJ.Framework.Exceptions;
using JJ.Business.Canonical;

namespace JJ.Business.SaveText
{
    public class TextSaver
    {
        private const int ENTITY_ID = 1;

        private IEntityRepository _entityRepository;

        public TextSaver(IEntityRepository entityRepository)
        {
            if (entityRepository == null) throw new NullException(() => entityRepository);
            _entityRepository = entityRepository;
        }

        public string GetText()
        {
            Entity entity = GetEntity();
            return entity.Text;
        }

        public VoidResult SaveText(string value)
        {
            var result = new VoidResult();

            IValidator validator = new TextValidator(value);
            if (!validator.IsValid)
            {
                result.Successful = false;
                result.Messages = validator.ValidationMessages.ToCanonical();
                return result;
            }

            Entity entity = GetEntity();
            entity.Text = value;
            _entityRepository.Update(entity);

            result.Messages = new List<JJ.Data.Canonical.Message>();
            result.Successful = true;
            return result;
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
