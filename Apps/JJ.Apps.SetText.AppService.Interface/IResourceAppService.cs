using JJ.Apps.SetText.AppService.Interface.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;

namespace JJ.Apps.SetText.AppService.Interface
{
    [ServiceContract]
    public interface IResourceAppService
    {
        [OperationContract]
        Messages GetMessages(string cultureName);

        [OperationContract]
        Labels GetLabels(string cultureName);

        [OperationContract]
        Titles GetTitles(string cultureName);
    }
}
