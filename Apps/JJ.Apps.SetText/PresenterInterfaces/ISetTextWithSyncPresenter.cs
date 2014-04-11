using JJ.Apps.SetText.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;

namespace JJ.Apps.SetText.PresenterInterfaces
{
    public interface ISetTextWithSyncPresenter
    {
        SetTextWithSyncViewModel Show();
        SetTextWithSyncViewModel Save(SetTextWithSyncViewModel viewModel);
        SetTextWithSyncViewModel Synchronize(SetTextWithSyncViewModel viewModel);
    }
}
