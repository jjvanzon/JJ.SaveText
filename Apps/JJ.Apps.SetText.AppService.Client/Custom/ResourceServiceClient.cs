using JJ.Apps.SetText.AppService.Interface;
using JJ.Apps.SetText.AppService.Interface.Models;
using JJ.Framework.Soap;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace JJ.Apps.SetText.AppService.Client.Custom
{
    public class ResourceAppServiceClient : IResourceAppService
    {
        private WcfSoapClient _serviceClient;

        public ResourceAppServiceClient(string url)
        {
            _serviceClient = new WcfSoapClient(url, typeof(IResourceAppService).Name);
        }

        public Messages GetMessages(string cultureName)
        {
            Messages messages = _serviceClient.Invoke<Messages>("GetMessages", new SoapParameter("cultureName", cultureName));
            return messages;
        }

        public Labels GetLabels(string cultureName)
        {
            Labels labels = _serviceClient.Invoke<Labels>("GetLabels", new SoapParameter("cultureName", cultureName));
            return labels;
        }

        public Titles GetTitles(string cultureName)
        {
            Titles titles = _serviceClient.Invoke<Titles>("GetTitles", new SoapParameter("cultureName", cultureName));
            return titles;
        }
    }
}
