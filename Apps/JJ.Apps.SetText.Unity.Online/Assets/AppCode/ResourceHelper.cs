using System;
using JJ.Apps.SetText.AppService.Interface;
using JJ.Apps.SetText.AppService.Interface.Models;
using JJ.Apps.SetText.AppService.Interface.CustomClient;

namespace JJ.Apps.SetText.Unity.Online
{
    public class ResourceHelper
    {
		private string _url = "http://83.82.26.17:6371/ResourceService.svc";

		private string _culture;

        public ResourceHelper(string cultureName)
        {
			LoadResources(cultureName);
        }

        public Labels Labels { get; private set; }
        public Titles Titles { get; private set; }
        public Messages Messages { get; private set; }

        private void LoadResources(string cultureName)
        {
			IResourceService service = CreateServiceClient ();
        	Labels = service.GetLabels(cultureName);
        	Titles = service.GetTitles(cultureName);
        	Messages = service.GetMessages(cultureName);
        }

		private IResourceService CreateServiceClient()
		{
			var client = new ResourceServiceClient (_url);
			return client;
		}
    }
}
