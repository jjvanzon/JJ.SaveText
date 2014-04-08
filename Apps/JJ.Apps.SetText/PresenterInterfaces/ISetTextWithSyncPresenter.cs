using JJ.Apps.SetText.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;

namespace JJ.Apps.SetText.PresenterInterfaces
{
    [ServiceContract]
    public interface ISetTextWithSyncPresenter
    {
        [OperationContract]
        SetTextWithSyncViewModel Show();

        [OperationContract]
        SetTextWithSyncViewModel Save(SetTextWithSyncViewModel viewModel);

        [OperationContract]
        SetTextWithSyncViewModel Synchronize(SetTextWithSyncViewModel viewModel);
    }
}
