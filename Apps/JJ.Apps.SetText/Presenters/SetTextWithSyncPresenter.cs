using JJ.Apps.SetText.ViewModels;
using JJ.Business.SetText;
using JJ.Models.SetText.Persistence.RepositoryInterfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using JJ.Apps.SetText.ViewModels.Helpers;
using Canonical = JJ.Models.Canonical;
using JJ.Models.Canonical;
using JJ.Apps.SetText.PresenterInterfaces;

namespace JJ.Apps.SetText.Presenters
{
    public class SetTextWithSyncPresenter : ISetTextWithSyncPresenter
    {
        private IEntityRepository _entityRepository;
        private TextSetter _textSetter;

        public SetTextWithSyncPresenter(IEntityRepository entityRepository)
        {
            if (entityRepository == null) throw new ArgumentNullException("entityRepository");

            _entityRepository = entityRepository;
            _textSetter = new TextSetter(entityRepository);
        }

        public SetTextWithSyncViewModel Show()
        {
            return CreateViewModel();
        }

        public SetTextWithSyncViewModel Save(SetTextWithSyncViewModel viewModel)
        {
            viewModel.NullCoallesce();

            Result saveResult = _textSetter.SetText(viewModel.Text);
            if (saveResult.Successful)
            {
                _entityRepository.Commit();
            }

            // TODO: In a stateful situation and when there are validation messages, this wil overwrite my original value!
            SetTextWithSyncViewModel viewModel2 = CreateViewModel();
            viewModel2.ValidationMessages = saveResult.ValidationMessages;
            viewModel2.TextWasSavedMessageVisible = saveResult.Successful;

            viewModel2.TextWasSavedButNotYetSynchronized = saveResult.Successful;

            return viewModel2;
        }

        /// <summary>
        /// When working in offline mode / local storage mode
        /// and you want to synchronize with the server,
        /// don't call this Synchronize method on the local presenter,
        /// but call the Synchronize method of the app service.
        /// </summary>
        public SetTextWithSyncViewModel Synchronize(SetTextWithSyncViewModel viewModel)
        {
            viewModel.NullCoallesce();

            Result syncResult = _textSetter.SetText(viewModel.Text);
            if (syncResult.Successful)
            {
                _entityRepository.Commit();
            }

            SetTextWithSyncViewModel viewModel2 = CreateViewModel();
            viewModel2.SyncValidationMessages = syncResult.ValidationMessages;
            viewModel2.SyncSuccessfulMessageVisible = syncResult.Successful;

            if (syncResult.Successful)
            {
                viewModel2.TextWasSavedButNotYetSynchronized = false;
            }

            return viewModel2;
        }

        private SetTextWithSyncViewModel CreateViewModel()
        {
            string text = _textSetter.GetText();
            var viewModel = new SetTextWithSyncViewModel
            {
                Text = text,
                ValidationMessages = new List<Canonical.ValidationMessage>(),
                SyncValidationMessages = new List<Canonical.ValidationMessage>()
            };
            return viewModel;
        }
    }
}