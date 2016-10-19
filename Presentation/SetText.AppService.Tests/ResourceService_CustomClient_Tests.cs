using JJ.Presentation.SetText.AppService.Client.Custom;
using JJ.Presentation.SetText.AppService.Interface.Models;
using JJ.Presentation.SetText.Interface.ViewModels;
using JJ.Framework.Configuration;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JJ.Presentation.SetText.AppService.Tests
{
    [TestClass]
    public class ResourceAppService_CustomClient_Tests
    {
        [TestMethod]
        public void Test_ResourceAppService_CustomClient()
        {
            string url = AppSettings<IAppSettings>.Get(x => x.ResourceAppServiceUrl);
            string cultureName = "nl-NL";
            
            var client = new ResourceAppServiceClient(url);
            Labels labels = client.GetLabels(cultureName);
            Messages messages = client.GetMessages(cultureName);
            Titles titles = client.GetTitles(cultureName);
        }
    }
}
