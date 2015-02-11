using JJ.Framework.Persistence;
using JJ.Framework.Reflection;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading;
using System.Web;

namespace JJ.Presentation.SetText.AppService.Helpers
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
