using System;
//using JJ.Apps.SetText.AppService.Models;
//using System.ServiceModel;

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
			using (ResourceService service = CreateServiceClient())
            {
                Labels = service.GetLabels(cultureName);
                Titles = service.GetTitles(cultureName);
                Messages = service.GetMessages(cultureName);
            }
        }

		/*
		private ResourceServiceClient CreateServiceClient()
		{
			ResourceServiceClient client = new ResourceServiceClient (
				new BasicHttpBinding(), 
				new EndpointAddress(_url));
			return client;
		}
		*/
		
		private ResourceService CreateServiceClient()
		{
			ResourceService client = new ResourceService ();
			client.Url = _url;
			return client;
		}
    }
}
