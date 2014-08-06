using JJ.Apps.SetText.AppService.Interface;
using JJ.Apps.SetText.AppService.Interface.Models;
using JJ.Framework.Soap;
using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;

namespace JJ.Apps.SetText.AppService.Client.Wcf
{
    public class ResourceAppServiceClient : ClientBase<IResourceAppService>, IResourceAppService
    {
        public ResourceAppServiceClient(string url)
            : base(new BasicHttpBinding(), new EndpointAddress(url))
        { }

        public Messages GetMessages(string cultureName)
        {
            Messages messages = Channel.GetMessages(cultureName);
            return messages;
        }

        public Labels GetLabels(string cultureName)
        {
            Labels labels = Channel.GetLabels(cultureName);
            return labels;
        }

        public Titles GetTitles(string cultureName)
        {
            Titles titles = Channel.GetTitles(cultureName);
            return titles;
        }
    }
}
