using JJ.Presentation.SaveText.AppService.Interface;
using JJ.Presentation.SaveText.Interface.PresenterInterfaces;
using JJ.Presentation.SaveText.Interface.ViewModels;
using JJ.Framework.Soap;
using System;

namespace JJ.Presentation.SaveText.AppService.Client.Custom
{
    // ISaveTextAppService differs from ISaveTextPresenter only in the additional parameters for the service methods.
    // You can pass those to the constructor of the client, expose the presenter interface, and pass the extra parameters to the Channel.

    public class SaveTextAppServiceClient : CustomWcfSoapClient<ISaveTextAppService>, ISaveTextPresenter
    {
        private string _cultureName;

        public SaveTextAppServiceClient(string url, string cultureName)
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
        public SaveTextAppServiceClient(string url, string cultureName, Func<string, string, string> sendMessageDelegate)
            : base(url, sendMessageDelegate)
        {
            _cultureName = cultureName;
        }

        public SaveTextViewModel Show()
        {
            return Invoke(x => x.Show(_cultureName));
        }

        public SaveTextViewModel Save(SaveTextViewModel viewModel)
        {
            return Invoke(x => x.Save(viewModel, _cultureName));
        }
    }
}
