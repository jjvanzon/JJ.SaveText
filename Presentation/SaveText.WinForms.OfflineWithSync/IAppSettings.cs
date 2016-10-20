using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JJ.Presentation.SaveText.WinForms.OfflineWithSync
{
    internal interface IAppSettings
    {
        string AppServiceUrl { get; }
        int SynchronizationTimerIntervalInMilliseconds { get; }
        int CheckServiceAvailabilityTimeoutInMilliseconds { get; }
    }
}
