using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace JJ.Presentation.SetText.WinForms.Online
{
    internal interface IAppSettings
    {
        string SetTextAppService { get; }
        string ResourceAppService { get; set; }
    }
}
