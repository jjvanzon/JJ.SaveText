using JJ.Presentation.SetText.Interface.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace JJ.Presentation.SetText.Interface.PresenterInterfaces
{
    public interface ISetTextWithSyncPresenter
    {
        SetTextViewModel Show();
        SetTextViewModel Save(SetTextViewModel viewModel);
        SetTextViewModel Synchronize(SetTextViewModel viewModel);
    }
}
