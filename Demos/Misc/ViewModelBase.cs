using System.Collections.Generic;

namespace JJ.Framework.Presentation
{
    /// <summary>
    /// Not finished. Trying something out.
    /// </summary>
    internal class ViewModelBase
    {
        public bool Successful { get; set; }
        public IList<string> Messages { get; set; }
        public int RefreshCounter { get; set; }
    }
}
