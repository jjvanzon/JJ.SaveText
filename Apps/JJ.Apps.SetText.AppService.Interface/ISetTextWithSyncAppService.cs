using JJ.Apps.SetText.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;

namespace JJ.Apps.SetText.AppService.Interface
{
    // AppService interfaces are not necessarily the same as the presenter interfaces,
    // for instance you might pass ExecutionContext with each call, containing culture and credentials.

    [ServiceContract]
    public interface ISetTextWithSyncAppService
    {
        [OperationContract]
        SetTextViewModel Show();

        [OperationContract]
        SetTextViewModel Save(SetTextViewModel viewModel);

        [OperationContract]
        SetTextViewModel Synchronize(SetTextViewModel viewModel);
    }
}
