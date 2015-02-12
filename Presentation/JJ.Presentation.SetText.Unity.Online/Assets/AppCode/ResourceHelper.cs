using System;
using JJ.Presentation.SetText.AppService.Interface;
using JJ.Presentation.SetText.AppService.Interface.Models;
using JJ.Presentation.SetText.AppService.Client.Custom;

namespace JJ.Presentation.SetText.Unity.Online
{
    public class ResourceHelper
    {
		private string _url = "http://83.82.26.17:6371/ResourceAppService.svc";

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
			IResourceAppService service = CreateServiceClient ();
        	Labels = service.GetLabels(cultureName);
        	Titles = service.GetTitles(cultureName);
        	Messages = service.GetMessages(cultureName);
        }

		private IResourceAppService CreateServiceClient()
		{
			var client = new ResourceAppServiceClient (_url);
			return client;
		}
    }
}
