using JJ.Apps.SetText.AppService.Interface;
using JJ.Apps.SetText.PresenterInterfaces;
using JJ.Apps.SetText.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;

namespace JJ.Apps.SetText.AppService.Interface.Clients
{
    // If ISetTextAppService differs from ISetTextPresenter it will probably only be additional parameters for the service methods.
    // You might want to pass those to the constructor of the client, expose the presenter interface, and pass the extra parameters to the Channel.

    public class SetTextAppServiceClient : ClientBase<ISetTextAppService>, ISetTextPresenter
    {
        public SetTextAppServiceClient(string url)
            : base(new BasicHttpBinding(), new EndpointAddress(url))
        { }

        public SetTextViewModel Show()
        {
            return Channel.Show();
        }

        public SetTextViewModel Save(SetTextViewModel viewModel)
        {
            return Channel.Save(viewModel);
        }
    }
}
