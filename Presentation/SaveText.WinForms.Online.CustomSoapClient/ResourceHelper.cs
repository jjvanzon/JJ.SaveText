using JJ.Framework.Configuration;
using JJ.Presentation.SaveText.AppService.Client.Custom;
using JJ.Presentation.SaveText.AppService.Interface.Models;

namespace JJ.Presentation.SaveText.WinForms.Online.CustomSoapClient
{
    internal static class ResourceHelper
    {
        static ResourceHelper() => LoadResources();

        public static Labels Labels { get; private set; }
        public static Titles Titles { get; private set; }
        public static Messages Messages { get; private set; }

        private static void LoadResources()
        {
            string url = AppSettingsReader<IAppSettings>.Get(x => x.ResourceAppService);

            var service = new ResourceAppServiceClient(url);
            string cultureName = GetCultureName();
            Labels = service.GetLabels(cultureName);
            Titles = service.GetTitles(cultureName);
            Messages = service.GetMessages(cultureName);
        }

        private static string GetCultureName() => System.Threading.Thread.CurrentThread.CurrentUICulture.Name;
    }
}
