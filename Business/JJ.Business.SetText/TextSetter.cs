using JJ.Business.SetText.Validation;
using JJ.Business.SetText.Helpers;
using JJ.Framework.Validation;
using JJ.Business.CanonicalModel;
using JJ.Data.SetText;
using JJ.Data.SetText.DefaultRepositories.RepositoryInterfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using JJ.Framework.Reflection.Exceptions;

namespace JJ.Business.SetText
{
    public class TextSetter
    {
        private const int ENTITY_ID = 1;

        private IEntityRepository _entityRepository;

        public TextSetter(IEntityRepository entityRepository)
        {
            if (entityRepository == null) throw new NullException(() => entityRepository);
            _entityRepository = entityRepository;
        }

        public string GetText()
        {
            Entity entity = GetEntity();
            return entity.Text;
        }

        public VoidResult SetText(string value)
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

            result.Messages = new List<JJ.Business.CanonicalModel.Message>();
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
