using JJ.Apps.SetText.ViewModels;
using JJ.Models.SetText.Persistence.RepositoryInterfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace JJ.Apps.SetText.Presenters
{
    public class SetTextWithSyncPresenter
    {
        // TODO: You might just want to copy-paste code from SetTextPresenter.

        private SetTextPresenter _basePresenter;

        public SetTextWithSyncPresenter(IEntityRepository entityRepository)
        {
            _basePresenter = new SetTextPresenter(entityRepository);
        }

        public SetTextWithSyncViewModel Show()
        {
            SetTextViewModel baseViewModel = _basePresenter.Show();
            return CreateViewModel(baseViewModel);
        }

        public SetTextWithSyncViewModel Save(SetTextWithSyncViewModel viewModel)
        {
            
            throw new NotImplementedException();
        }

        public SetTextWithSyncViewModel Synchronize(SetTextWithSyncViewModel viewModel)
        {
            throw new NotImplementedException();
        }

        private SetTextWithSyncViewModel CreateViewModel(SetTextViewModel baseViewModel)
        {
            return new SetTextWithSyncViewModel
            {
                Text = baseViewModel.Text,
                TextWasSavedMessageVisible = baseViewModel.TextWasSavedMessageVisible,
                ValidationMessages = baseViewModel.ValidationMessages
            };
        }
    }
}
