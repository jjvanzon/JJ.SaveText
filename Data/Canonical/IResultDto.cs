using System.Collections.Generic;

namespace JJ.Data.Canonical
{
    public interface IResultDto
    {
        bool Successful { get; set; }
        IList<string> Messages { get; set; }
    }
}
