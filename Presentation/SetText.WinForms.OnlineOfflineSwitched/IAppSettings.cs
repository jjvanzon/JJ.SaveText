using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JJ.Presentation.SaveText.WinForms.OnlineOfflineSwitched
{
    internal interface IAppSettings
    {
        string SaveTextWithSyncAppServiceUrl { get; }
        string SaveTextAppServiceUrl { get; }
    }
}
