using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JJ.Presentation.SetText.AppService.Tests
{
    internal interface IAppSettings
    {
        string SetTextAppServiceUrl { get; }
        string SetTextWithSyncAppServiceUrl { get; }
        string ResourceAppServiceUrl { get; }
        string CultureName { get; }
    }
}
