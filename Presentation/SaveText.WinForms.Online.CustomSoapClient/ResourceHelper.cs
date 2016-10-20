using JJ.Presentation.SaveText.AppService.Client.Custom;
using JJ.Presentation.SaveText.AppService.Interface.Models;
using JJ.Framework.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JJ.Presentation.SaveText.WinForms.Online
{
    internal static class ResourceHelper
    {
        static ResourceHelper()
        {
            LoadResources();
        }

        public static Labels Labels { get; private set; }
        public static Titles Titles { get; private set; }
        public static Messages Messages { get; private set; }

        private static void LoadResources()
        {
            string url = AppSettings<IAppSettings>.Get(x => x.ResourceAppService);

            var service = new ResourceAppServiceClient(url);
            string cultureName = GetCultureName();
            Labels = service.GetLabels(cultureName);
            Titles = service.GetTitles(cultureName);
            Messages = service.GetMessages(cultureName);
        }

        private static string GetCultureName()
        {
            return System.Threading.Thread.CurrentThread.CurrentUICulture.Name;
        }
    }
}
