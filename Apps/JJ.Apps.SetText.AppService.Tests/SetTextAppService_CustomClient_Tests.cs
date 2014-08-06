using JJ.Apps.SetText.AppService.Client.Custom;
using JJ.Apps.SetText.Interface.ViewModels;
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
    public class SetTextAppService_CustomClient_Tests
    {
        [TestMethod]
        public void Test_SetTextAppService_CustomClient()
        {
            string url = AppSettings<IAppSettings>.Get(x => x.SetTextAppServiceUrl);
            string cultureName = AppSettings<IAppSettings>.Get(x => x.CultureName);

            var client = new SetTextAppServiceClient(url, cultureName);

            SetTextViewModel viewModel = client.Show();
            SetTextViewModel viewModel2 = client.Save(viewModel);
        }
    }
}
