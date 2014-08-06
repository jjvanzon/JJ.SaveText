using JJ.Apps.SetText.Interface.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace JJ.Apps.SetText.Interface.PresenterInterfaces
{
    public interface ISetTextWithSyncPresenter
    {
        SetTextViewModel Show();
        SetTextViewModel Save(SetTextViewModel viewModel);
        SetTextViewModel Synchronize(SetTextViewModel viewModel);
    }
}
