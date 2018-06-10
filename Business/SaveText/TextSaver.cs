using System.Collections.Generic;
using JJ.Business.Canonical;
using JJ.Business.SaveText.Validation;
using JJ.Data.Canonical;
using JJ.Data.SaveText;
using JJ.Data.SaveText.DefaultRepositories.RepositoryInterfaces;
using JJ.Framework.Exceptions.Basic;
using JJ.Framework.Validation;

namespace JJ.Business.SaveText
{
    public class TextSaver
    {
        private const int ENTITY_ID = 1;

        private readonly IEntityRepository _entityRepository;

        public TextSaver(IEntityRepository entityRepository)
        {
            _entityRepository = entityRepository ?? throw new NullException(() => entityRepository);
        }

        public string GetText()
        {
            Entity entity = GetEntity();
            return entity.Text;
        }

        public VoidResultDto SaveText(string value)
        {
            var result = new VoidResultDto();

            IValidator validator = new TextValidator(value);
            if (!validator.IsValid)
            {
                return validator.ToCanonical();
            }

            Entity entity = GetEntity();
            entity.Text = value;
            _entityRepository.Update(entity);

            result.Messages = new List<string>();
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