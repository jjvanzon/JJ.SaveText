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
    public interface ISetTextAppService
    {
        [OperationContract]
        SetTextViewModel Show();

        [OperationContract]
        SetTextViewModel Save(SetTextViewModel viewModel);
    }
}
