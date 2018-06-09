using System.Collections.Generic;
using JJ.Business.SaveText;
using JJ.Data.Canonical;
using JJ.Data.SaveText.DefaultRepositories.RepositoryInterfaces;
using JJ.Framework.Exceptions.Basic;
using JJ.Presentation.SaveText.Helpers;
using JJ.Presentation.SaveText.Interface.PresenterInterfaces;
using JJ.Presentation.SaveText.Interface.ViewModels;

namespace JJ.Presentation.SaveText.Presenters
{
    public class SaveTextWithSyncPresenter : ISaveTextWithSyncPresenter
    {
        private readonly IEntityRepository _entityRepository;
        private readonly TextSaver _textSetter;

        public SaveTextWithSyncPresenter(IEntityRepository entityRepository)
        {
            _entityRepository = entityRepository ?? throw new NullException(() => entityRepository);
            _textSetter = new TextSaver(entityRepository);
        }

        public SaveTextViewModel Show()
        {
            return CreateViewModel();
        }

        public SaveTextViewModel Save(SaveTextViewModel viewModel)
        {
            viewModel.NullCoallesce();

            VoidResultDto saveResult = _textSetter.SaveText(viewModel.Text);
            if (saveResult.Successful)
            {
                _entityRepository.Commit();
                SaveTextViewModel viewModel2 = CreateViewModel();
                viewModel2.TextWasSavedMessageVisible = true;
                viewModel2.TextWasSavedButNotYetSynchronized = saveResult.Successful;
                return viewModel2;
            }
            else
            {
                SaveTextViewModel viewModel2 = CreateViewModel();
                viewModel2.TextWasSavedMessageVisible = false;
                viewModel2.ValidationMessages = saveResult.Messages;
                viewModel2.Text = viewModel.Text; // Keep entered value.
                return viewModel2;
            }
        }

        /// <summary>
        ///     When working in offline mode / local storage mode
        ///     and you want to synchronize with the server,
        ///     don't call this Synchronize method on the local presenter,
        ///     but call the Synchronize method of the app service.
        /// </summary>
        public SaveTextViewModel Synchronize(SaveTextViewModel viewModel)
        {
            viewModel.NullCoallesce();

            VoidResultDto syncResult = _textSetter.SaveText(viewModel.Text);
            if (syncResult.Successful)
            {
                _entityRepository.Commit();
                SaveTextViewModel viewModel2 = CreateViewModel();
                viewModel2.ValidationMessages = syncResult.Messages;
                viewModel2.SyncSuccessfulMessageVisible = true;
                viewModel2.TextWasSavedButNotYetSynchronized = false;
                return viewModel2;
            }
            else
            {
                SaveTextViewModel viewModel2 = CreateViewModel();
                viewModel2.ValidationMessages = syncResult.Messages;
                viewModel2.SyncSuccessfulMessageVisible = false;
                viewModel2.Text = viewModel.Text; // Keep entered value.
                return viewModel2;
            }
        }

        private SaveTextViewModel CreateViewModel()
        {
            string text = _textSetter.GetText();
            var viewModel = new SaveTextViewModel
            {
                Text = text,
                ValidationMessages = new List<string>()
            };
            return viewModel;
        }
    }
}