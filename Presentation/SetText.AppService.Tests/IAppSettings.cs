using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JJ.Presentation.SaveText.AppService.Tests
{
    internal interface IAppSettings
    {
        string SaveTextAppServiceUrl { get; }
        string SaveTextWithSyncAppServiceUrl { get; }
        string ResourceAppServiceUrl { get; }
        string CultureName { get; }
    }
}
