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
    // ISetTextAppService differs from ISetTextPresenter only in the additional parameters for the service methods.
    // You can pass those to the constructor of the client, expose the presenter interface, and pass the extra parameters to the Channel.

    public class SetTextAppServiceClient : WcfSoapClient<ISetTextAppService>, ISetTextPresenter
    {
        private string _cultureName;

        public SetTextAppServiceClient(string url, string cultureName)
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
    }
}
