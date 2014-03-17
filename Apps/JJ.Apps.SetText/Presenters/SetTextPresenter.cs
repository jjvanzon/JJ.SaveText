using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using JJ.Framework.Validation;
using JJ.Models.SetText;
using JJ.Models.SetText.Persistence.RepositoryInterfaces;
using JJ.Business.SetText.Validation;
using JJ.Apps.SetText.Presenters.Helpers;
using JJ.Apps.SetText.ViewModels;
using JJ.Apps.SetText.ViewModels.Helpers;
using JJ.Business.SetText;
using Canonical = JJ.Models.Canonical;

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
            string text = _textSetter.GetText();
            var viewModel = new SetTextViewModel
            {
                Text = text,
                ValidationMessages = new List<Canonical.ValidationMessage>()
            };
            return viewModel;
        }

        public SetTextViewModel Save(SetTextViewModel viewModel)
        {
            viewModel.NullCoallesce();

            _textSetter.SetText(viewModel.Text);

            IValidator validator = new TextValidator(viewModel.Text);
            if (!validator.IsValid)
            {
                viewModel.ValidationMessages = validator.ValidationMessages.ToCanonical();
                return viewModel;
            }
            else
            {
                viewModel.TextWasSavedMessageVisible = true;
                _entityRepository.Commit();
            }
            return viewModel;
        }
    }
}
