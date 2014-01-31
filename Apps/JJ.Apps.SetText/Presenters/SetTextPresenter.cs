using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using JJ.Framework.Validation;
using JJ.Models.SetText;
using JJ.Models.SetText.Persistence.RepositoryInterfaces;
using JJ.Business.SetText.Validation;
using JJ.Apps.SetText.Presenters.Helpers;
using JJ.Apps.SetText.ViewModels;
using JJ.Apps.SetText.ViewModels.Helpers;
using JJ.Business.SetText;

namespace JJ.Apps.SetText.Presenters
{
    public class SetTextPresenter
    {
        private IEntityRepository _entityRepository;
        private TextSetter _textSetter;

        public SetTextPresenter(IEntityRepository entityRepository)
        {
            if (entityRepository == null) throw new ArgumentNullException("entityRepository");

            _entityRepository = entityRepository;
            _textSetter = new TextSetter(entityRepository);
        }

        public SetTextViewModel Show()
        {
            Entity entity = _textSetter.GetEntity();
            SetTextViewModel viewModel = entity.ToSetTextViewModel();
            return viewModel;
        }

        public SetTextViewModel Save(SetTextViewModel viewModel)
        {
            // Get entity with viewmodel applied to it.
            Entity entity = viewModel.ToEntity(_textSetter);

            // Create new, complete viewmodel from entity
            SetTextViewModel viewModel2 = entity.ToSetTextViewModel();

            // Validate
            IValidator validator = new EntityValidator(entity);
            if (!validator.IsValid)
            {
                viewModel2.ValidationMessages = validator.ValidationMessages.ToCanonical();
                return viewModel2;
            }

            viewModel2.TextWasSavedMessageVisible = true;

            // Commit
            _entityRepository.Commit();

            return viewModel2;
        }
    }
}
