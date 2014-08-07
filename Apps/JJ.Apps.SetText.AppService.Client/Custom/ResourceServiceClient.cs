using JJ.Apps.SetText.AppService.Interface;
using JJ.Apps.SetText.AppService.Interface.Models;
using JJ.Framework.Soap;
using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;

namespace JJ.Apps.SetText.AppService.Client.Custom
{
    public class ResourceAppServiceClient : WcfSoapClient<IResourceAppService>, IResourceAppService
    {
        public ResourceAppServiceClient(string url)
            : base(url)
        { }

        public Messages GetMessages(string cultureName)
        {
            Messages messages = Invoke(x => x.GetMessages(cultureName));
            return messages;
        }

        public Labels GetLabels(string cultureName)
        {
            Labels labels = Invoke(x => x.GetLabels(cultureName));
            return labels;
        }

        public Titles GetTitles(string cultureName)
        {
            Titles titles = Invoke(x => x.GetTitles(cultureName));
            return titles;
        }
    }
}
