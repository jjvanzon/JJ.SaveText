using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Canonical = JJ.Business.CanonicalModel;
using JJ.Business.CanonicalModel;
using JJ.Data.SetText.DefaultRepositories.RepositoryInterfaces;
using JJ.Business.SetText;
using JJ.Presentation.SetText.Interface.ViewModels;
using JJ.Presentation.SetText.Interface.PresenterInterfaces;
using JJ.Presentation.SetText.Helpers;
using JJ.Framework.Reflection.Exceptions;

namespace JJ.Presentation.SetText.Presenters
{
    public class SetTextWithSyncPresenter : ISetTextWithSyncPresenter
    {
        private IEntityRepository _entityRepository;
        private TextSetter _textSetter;

        public SetTextWithSyncPresenter(IEntityRepository entityRepository)
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

            VoidResult saveResult = _textSetter.SetText(viewModel.Text);
            if (saveResult.Successful)
            {
                _entityRepository.Commit();
                SetTextViewModel viewModel2 = CreateViewModel();
                viewModel2.TextWasSavedMessageVisible = true;
                viewModel2.TextWasSavedButNotYetSynchronized = saveResult.Successful;
                return viewModel2;
            }
            else
            {
                SetTextViewModel viewModel2 = CreateViewModel();
                viewModel2.TextWasSavedMessageVisible = false;
                viewModel2.ValidationMessages = saveResult.ValidationMessages;
                viewModel2.Text = viewModel.Text; // Keep entered value.
                return viewModel2;
            }
        }

        /// <summary>
        /// When working in offline mode / local storage mode
        /// and you want to synchronize with the server,
        /// don't call this Synchronize method on the local presenter,
        /// but call the Synchronize method of the app service.
        /// </summary>
        public SetTextViewModel Synchronize(SetTextViewModel viewModel)
        {
            viewModel.NullCoallesce();

            VoidResult syncResult = _textSetter.SetText(viewModel.Text);
            if (syncResult.Successful)
            {
                _entityRepository.Commit();
                SetTextViewModel viewModel2 = CreateViewModel();
                viewModel2.ValidationMessages = syncResult.ValidationMessages;
                viewModel2.SyncSuccessfulMessageVisible = true;
                viewModel2.TextWasSavedButNotYetSynchronized = false;
                return viewModel2;
            }
            else
            {
                SetTextViewModel viewModel2 = CreateViewModel();
                viewModel2.ValidationMessages = syncResult.ValidationMessages;
                viewModel2.SyncSuccessfulMessageVisible = false;
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
                ValidationMessages = new List<Canonical.ValidationMessage>(),
            };
            return viewModel;
        }
    }
}