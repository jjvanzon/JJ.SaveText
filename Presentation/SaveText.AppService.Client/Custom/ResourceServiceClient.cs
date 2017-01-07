using JJ.Presentation.SaveText.AppService.Interface;
using JJ.Presentation.SaveText.AppService.Interface.Models;
using JJ.Framework.Soap;
using System;

namespace JJ.Presentation.SaveText.AppService.Client.Custom
{
    public class ResourceAppServiceClient : CustomWcfSoapClient<IResourceAppService>, IResourceAppService
    {
        public ResourceAppServiceClient(string url)
            : base(url)
        { }

        /// <param name="sendMessageDelegate">
        /// You can handle the sending of the SOAP message and the receiving of the response yourself
        /// by passing this sendMessageDelegate. This is for environments that do not support HttpWebRequest.
        /// First parameter of the delegate is SOAP action, second parameter is SOAP message as an XML string,
        /// return value should be text received.
        /// </param>
        public ResourceAppServiceClient(string url, Func<string, string, string> sendMessageDelegate)
            : base(url, sendMessageDelegate)
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
