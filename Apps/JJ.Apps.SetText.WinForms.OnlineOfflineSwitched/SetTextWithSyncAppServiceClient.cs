using JJ.Apps.SetText.PresenterInterfaces;
using JJ.Apps.SetText.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;

namespace JJ.Apps.SetText.WinForms.OnlineOfflineSwitched
{
    public class SetTextWithSyncAppServiceClient : ClientBase<ISetTextWithSyncPresenter>, ISetTextWithSyncPresenter
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
