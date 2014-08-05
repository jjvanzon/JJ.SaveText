using JJ.Apps.SetText.AppService.Interface.CustomClient;
using JJ.Apps.SetText.ViewModels;
using JJ.Framework.Configuration;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JJ.Apps.SetText.AppService.Tests
{
    [TestClass]
    public class SetTextWithSyncAppService_CustomClient_Tests
    {
        [TestMethod]
        public void Test_SetTextWithSyncAppService_CustomClient()
        {
            string url = AppSettings<IAppSettings>.Get(x => x.SetTextWithSyncAppServiceUrl);
            string cultureName = AppSettings<IAppSettings>.Get(x => x.CultureName);

            var client = new SetTextWithSyncAppServiceClient(url, cultureName);

            SetTextViewModel viewModel = client.Show();
            SetTextViewModel viewModel2 = client.Save(viewModel);
            SetTextViewModel viewModel3 = client.Synchronize(viewModel);
        }
    }
}
