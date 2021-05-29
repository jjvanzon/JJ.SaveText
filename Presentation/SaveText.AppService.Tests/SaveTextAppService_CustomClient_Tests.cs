using System.Net;
using JJ.Presentation.SaveText.AppService.Client.Custom;
using JJ.Presentation.SaveText.Interface.ViewModels;
using JJ.Framework.Configuration;
using Microsoft.VisualStudio.TestTools.UnitTesting;
// ReSharper disable UnusedVariable

namespace JJ.Presentation.SaveText.AppService.Tests
{
    [TestClass]
    public class SaveTextAppService_CustomClient_Tests
    {
        [TestMethod]
        public void Test_SaveTextAppService_CustomClient()
        {
            try
            {
                string url = AppSettingsReader<IAppSettings>.Get(x => x.SaveTextAppServiceUrl);
                string cultureName = AppSettingsReader<IAppSettings>.Get(x => x.CultureName);

                var client = new SaveTextAppServiceClient(url, cultureName);

                SaveTextViewModel viewModel = client.Show();
                SaveTextViewModel viewModel2 = client.Save(viewModel);
            }
            catch (WebException ex)
            {
                Assert.Inconclusive(ex.Message);
            }
        }
    }
}