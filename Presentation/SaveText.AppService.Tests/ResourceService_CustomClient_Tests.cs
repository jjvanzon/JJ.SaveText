using JJ.Presentation.SaveText.AppService.Client.Custom;
using JJ.Presentation.SaveText.AppService.Interface.Models;
using JJ.Framework.Configuration;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace JJ.Presentation.SaveText.AppService.Tests
{
    [TestClass]
    public class ResourceAppService_CustomClient_Tests
    {
        [TestMethod]
        public void Test_ResourceAppService_CustomClient()
        {
            string url = AppSettingsReader<IAppSettings>.Get(x => x.ResourceAppServiceUrl);
            string cultureName = "nl-NL";
            
            var client = new ResourceAppServiceClient(url);
            Labels labels = client.GetLabels(cultureName);
            Messages messages = client.GetMessages(cultureName);
            Titles titles = client.GetTitles(cultureName);
        }
    }
}
