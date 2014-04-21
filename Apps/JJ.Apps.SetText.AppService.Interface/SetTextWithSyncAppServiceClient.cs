using JJ.Apps.SetText.AppService.Interface;
using JJ.Apps.SetText.PresenterInterfaces;
using JJ.Apps.SetText.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;

namespace JJ.Apps.SetText.AppService.Interface
{
    // If ISetTextAppService differs from ISetTextPresenter it will probably only be additional parameters for the service methods.
    // You might want to pass those to the constructor of the client, expose the presenter interface, and pass the extra parameters to the Channel.

    public class SetTextWithSyncAppServiceClient : ClientBase<ISetTextWithSyncAppService>, ISetTextWithSyncPresenter
    {
        public SetTextWithSyncAppServiceClient(string url)
            : base(new BasicHttpBinding(), new EndpointAddress(url))
        { }

        public SetTextWithSyncViewModel Show()
        {
            return Channel.Show();
        }

        public SetTextWithSyncViewModel Save(SetTextWithSyncViewModel viewModel)
        {
            return Channel.Save(viewModel);
        }

        public SetTextWithSyncViewModel Synchronize(SetTextWithSyncViewModel viewModel)
        {
            return Channel.Synchronize(viewModel);
        }
    }
}
