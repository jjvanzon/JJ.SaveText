using JJ.Apps.SetText.AppService.Interface.Models;
using JJ.Framework.Soap;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace JJ.Apps.SetText.AppService.Interface.CustomClient
{
    public class ResourceServiceClient : IResourceService
    {
        private WcfSoapClient _serviceClient;

        public ResourceServiceClient(string url)
        {
            _serviceClient = new WcfSoapClient(url, typeof(IResourceService).Name);
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
