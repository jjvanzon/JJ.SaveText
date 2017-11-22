using JJ.Framework.Exceptions;
using System.Globalization;
using System.Threading;

namespace JJ.Presentation.SaveText.AppService.Helpers
{
	internal static class ServiceHelper
	{
		public static void SetCulture(string cultureName)
		{
			if (cultureName == null) throw new NullException(() => cultureName);

			CultureInfo cultureInfo = new CultureInfo(cultureName);
			Thread.CurrentThread.CurrentCulture = cultureInfo;
			Thread.CurrentThread.CurrentUICulture = cultureInfo;
		}
	}
}
