using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JJ.Presentation.SetText.WinForms.OnlineOfflineSwitched
{
    internal interface IAppSettings
    {
        string SetTextWithSyncAppServiceUrl { get; }
        string SetTextAppServiceUrl { get; }
    }
}
