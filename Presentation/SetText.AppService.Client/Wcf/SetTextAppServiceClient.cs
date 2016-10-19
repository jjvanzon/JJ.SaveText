using JJ.Presentation.SetText.AppService.Interface;
using JJ.Presentation.SetText.Interface.PresenterInterfaces;
using JJ.Presentation.SetText.Interface.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;

namespace JJ.Presentation.SetText.AppService.Client.Wcf
{
    // ISetTextAppService differs from ISetTextPresenter only in the additional parameters for the service methods.
    // You can pass those to the constructor of the client, expose the presenter interface, and pass the extra parameters to the Channel.

    public class SetTextAppServiceClient : ClientBase<ISetTextAppService>, ISetTextPresenter
    {
        private string _cultureName;

        public SetTextAppServiceClient(string url, string cultureName)
            : base(new BasicHttpBinding(), new EndpointAddress(url))
        {
            _cultureName = cultureName;
        }

        public SetTextViewModel Show()
        {
            return Channel.Show(_cultureName);
        }

        public SetTextViewModel Save(SetTextViewModel viewModel)
        {
            return Channel.Save(viewModel, _cultureName);
        }
    }
}
