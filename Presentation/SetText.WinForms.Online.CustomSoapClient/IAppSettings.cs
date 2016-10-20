using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace JJ.Presentation.SaveText.WinForms.Online
{
    internal interface IAppSettings
    {
        string SaveTextAppService { get; }
        string ResourceAppService { get; set; }
    }
}
