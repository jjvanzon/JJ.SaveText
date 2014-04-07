using JJ.Apps.SetText.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;

namespace JJ.Apps.SetText.AppService
{
    [ServiceContract]
    public interface ISetTextWithSyncAppService
    {
        [OperationContract]
        SetTextWithSyncViewModel Show();

        [OperationContract]
        SetTextWithSyncViewModel Save(SetTextWithSyncViewModel viewModel);

        [OperationContract]
        SetTextWithSyncViewModel Synchronize(SetTextWithSyncViewModel viewModel);
    }
}
