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
    public class SetTextWithSyncAppServiceClient : CustomWcfSoapClient<ISetTextWithSyncAppService>, ISetTextWithSyncPresenter
    {
        private string _cultureName;

        public SetTextWithSyncAppServiceClient(string url, string cultureName)
            : base(url)
        {
            _cultureName = cultureName;
        }

        /// <param name="sendMessageDelegate">
        /// You can handle the sending of the SOAP message and the receiving of the response yourself
        /// by passing this sendMessageDelegate. This is for environments that do not support HttpWebRequest.
        /// First parameter of the delegate is SOAP action, second parameter is SOAP message as an XML string,
        /// return value should be text received.
        /// </param>
        public SetTextWithSyncAppServiceClient(string url, string cultureName, Func<string, string, string> sendMessageDelegate)
            : base(url, sendMessageDelegate)
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
