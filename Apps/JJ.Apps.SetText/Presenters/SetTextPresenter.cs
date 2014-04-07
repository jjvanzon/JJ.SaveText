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
using JJ.Models.Canonical;

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
            return CreateViewModel();
        }

        public SetTextViewModel Save(SetTextViewModel viewModel)
        {
            viewModel.NullCoallesce();

            Result result = _textSetter.SetText(viewModel.Text);
            if (result.Successful)
            {
                _entityRepository.Commit();
            }

            SetTextViewModel viewModel2 = CreateViewModel();
            viewModel2.ValidationMessages = result.ValidationMessages;
            viewModel2.TextWasSavedMessageVisible = result.Successful;
            return viewModel2;
        }

        private SetTextViewModel CreateViewModel()
        {
            string text = _textSetter.GetText();
            var viewModel = new SetTextViewModel
            {
                Text = text,
                ValidationMessages = new List<Canonical.ValidationMessage>()
            };
            return viewModel;
        }
    }
}
