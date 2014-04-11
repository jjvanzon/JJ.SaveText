using JJ.Apps.SetText.AppService.Interface;
using JJ.Apps.SetText.AppService.Interface.Models;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Reflection;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using System.Threading;

namespace JJ.Apps.SetText.AppService
{
    public class ResourceService : IResourceService
    {
        public Messages GetMessages(string cultureName)
        {
            return ConvertResources<Resources.Messages, Messages>(cultureName);
        }

        public Labels GetLabels(string cultureName)
        {
            return ConvertResources<Resources.Labels, Labels>(cultureName);
        }

        public Titles GetTitles(string cultureName)
        {
            return ConvertResources<Resources.Titles, Titles>(cultureName);
        }

        private TDest ConvertResources<TSource, TDest>(string cultureName)
            where TDest : new()
        {
            SetCulture(cultureName);

            var dest = new TDest();

            foreach (PropertyInfo property in typeof(TSource).GetProperties(BindingFlags.Public | BindingFlags.Static))
            {
                PropertyInfo property2 = typeof(TDest).GetProperty(property.Name, BindingFlags.Instance | BindingFlags.Public);

                if (property2 != null)
                {
                    property2.SetValue(dest, property.GetValue(null));
                }
            }

            return dest;
        }

        private void SetCulture(string cultureName)
        {
            CultureInfo cultureInfo; ;

            if (!String.IsNullOrEmpty(cultureName))
            {
                cultureInfo = CultureInfo.GetCultureInfo(cultureName);
            }
            else
            {
                cultureInfo = CultureInfo.InvariantCulture;
            }

            Thread.CurrentThread.CurrentCulture = cultureInfo;
            Thread.CurrentThread.CurrentUICulture = cultureInfo;
        }
    }
}
