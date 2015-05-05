using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using JJ.Framework.Validation;
using JJ.Data.SetText;
using JJ.Data.SetText.DefaultRepositories.RepositoryInterfaces;
using JJ.Business.SetText.Validation;
using JJ.Presentation.SetText.Interface.ViewModels;
using JJ.Presentation.SetText.Helpers;
using JJ.Business.SetText;
using Canonical = JJ.Business.CanonicalModel;
using JJ.Business.CanonicalModel;
using JJ.Presentation.SetText.Interface.PresenterInterfaces;
using JJ.Framework.Reflection.Exceptions;

namespace JJ.Presentation.SetText.Presenters
{
    public class SetTextPresenter : ISetTextPresenter
    {
        private IEntityRepository _entityRepository;
        private TextSetter _textSetter;

        public SetTextPresenter(IEntityRepository entityRepository)
        {
            if (entityRepository == null) throw new NullException(() => entityRepository);

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

            VoidResult result = _textSetter.SetText(viewModel.Text);
            if (result.Successful)
            {
                _entityRepository.Commit();
                SetTextViewModel viewModel2 = CreateViewModel();
                viewModel2.TextWasSavedMessageVisible = true;
                return viewModel2;
            }
            else
            {
                SetTextViewModel viewModel2 = CreateViewModel();
                viewModel2.ValidationMessages = result.Messages;
                viewModel2.TextWasSavedMessageVisible = false;
                viewModel2.Text = viewModel.Text; // Keep entered value.
                return viewModel2;
            }
        }

        private SetTextViewModel CreateViewModel()
        {
            string text = _textSetter.GetText();
            var viewModel = new SetTextViewModel
            {
                Text = text,
                ValidationMessages = new List<Canonical.Message>()
            };
            return viewModel;
        }
    }
}
