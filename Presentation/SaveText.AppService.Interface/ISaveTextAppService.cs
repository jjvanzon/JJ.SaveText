using JJ.Presentation.SaveText.Interface.ViewModels;
using System.ServiceModel;

namespace JJ.Presentation.SaveText.AppService.Interface
{
    // AppService interfaces are not necessarily the same as the presenter interfaces,
    // for instance you might pass ExecutionContext with each call, containing culture and credentials.

    [ServiceContract]
    public interface ISaveTextAppService
    {
        [OperationContract]
        SaveTextViewModel Show(string cultureName);

        [OperationContract]
        SaveTextViewModel Save(SaveTextViewModel viewModel, string cultureName);
    }
}
