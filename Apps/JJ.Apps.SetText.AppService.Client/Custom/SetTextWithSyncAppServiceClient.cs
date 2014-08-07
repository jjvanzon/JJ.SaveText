using JJ.Apps.SetText.AppService.Interface;
using JJ.Apps.SetText.Interface.PresenterInterfaces;
using JJ.Apps.SetText.Interface.ViewModels;
using JJ.Framework.Soap;
using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;

namespace JJ.Apps.SetText.AppService.Client.Custom
{
    public class SetTextWithSyncAppServiceClient : WcfSoapClient<ISetTextWithSyncAppService>, ISetTextWithSyncPresenter
    {
        private string _cultureName;

        public SetTextWithSyncAppServiceClient(string url, string cultureName)
            : base(url)
        {
            _cultureName = cultureName;
        }

        public SetTextViewModel Show()
        {
            return Invoke(x => x.Show(_cultureName));
        }

        public SetTextViewModel Save(SetTextViewModel viewModel)
        {
            return Invoke(x => x.Save(viewModel, _cultureName));
        }

        public SetTextViewModel Synchronize(SetTextViewModel viewModel)
        {
            return Invoke(x => x.Synchronize(viewModel, _cultureName));
        }
    }
}
