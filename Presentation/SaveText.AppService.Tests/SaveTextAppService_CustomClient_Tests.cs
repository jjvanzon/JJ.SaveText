using JJ.Presentation.SaveText.AppService.Client.Custom;
using JJ.Presentation.SaveText.Interface.ViewModels;
using JJ.Framework.Configuration;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JJ.Presentation.SaveText.AppService.Tests
{
    [TestClass]
    public class SaveTextAppService_CustomClient_Tests
    {
        [TestMethod]
        public void Test_SaveTextAppService_CustomClient()
        {
            string url = AppSettings<IAppSettings>.Get(x => x.SaveTextAppServiceUrl);
            string cultureName = AppSettings<IAppSettings>.Get(x => x.CultureName);

            var client = new SaveTextAppServiceClient(url, cultureName);

            SaveTextViewModel viewModel = client.Show();
            SaveTextViewModel viewModel2 = client.Save(viewModel);
        }
    }
}
