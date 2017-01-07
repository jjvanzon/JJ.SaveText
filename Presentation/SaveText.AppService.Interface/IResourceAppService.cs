using JJ.Presentation.SaveText.AppService.Interface.Models;
using System.ServiceModel;

namespace JJ.Presentation.SaveText.AppService.Interface
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
