using JJ.Apps.SetText.AppService.Interface;
using JJ.Apps.SetText.Interface.PresenterInterfaces;
using JJ.Apps.SetText.Interface.ViewModels;
using JJ.Framework.Soap;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace JJ.Apps.SetText.AppService.Client.Custom
{
    public class SetTextAppServiceClient : ISetTextPresenter
    {
        private WcfSoapClient _serviceClient;
        private string _cultureName;

        public SetTextAppServiceClient(string url, string cultureName)
        {
            _serviceClient = new WcfSoapClient(url, typeof(ISetTextAppService).Name);
            _cultureName = cultureName;
        }

        public SetTextViewModel Show()
        {
            SetTextViewModel viewModel = _serviceClient.Invoke<SetTextViewModel>("Show", new SoapParameter("cultureName", _cultureName));
            return viewModel;
        }

        public SetTextViewModel Save(SetTextViewModel viewModel)
        {
            SetTextViewModel viewModel2 = _serviceClient.Invoke<SetTextViewModel>("Save", new SoapParameter("viewModel", viewModel), new SoapParameter("cultureName", _cultureName));
            return viewModel2;
        }
    }
}
