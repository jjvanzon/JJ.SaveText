using System.Net;
using JJ.Presentation.SaveText.AppService.Client.Custom;
using JJ.Presentation.SaveText.Interface.ViewModels;
using JJ.Framework.Configuration;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace JJ.Presentation.SaveText.AppService.Tests
{
    [TestClass]
    public class SaveTextWithSyncAppService_CustomClient_Tests
    {
        [TestMethod]
        public void Test_SaveTextWithSyncAppService_CustomClient()
        {
            try
            {
                string url = AppSettingsReader<IAppSettings>.Get(x => x.SaveTextWithSyncAppServiceUrl);
                string cultureName = AppSettingsReader<IAppSettings>.Get(x => x.CultureName);

                var client = new SaveTextWithSyncAppServiceClient(url, cultureName);

                SaveTextViewModel viewModel = client.Show();
                SaveTextViewModel viewModel2 = client.Save(viewModel);
                SaveTextViewModel viewModel3 = client.Synchronize(viewModel);
            }
            catch (WebException ex)
            {
                Assert.Inconclusive(ex.Message);
            }
        }
    }
}