using System.Collections.Generic;

namespace JJ.Data.Canonical
{
    public class VoidResultDto : IResultDto
    {
        public bool Successful { get; set; }
        public IList<MessageDto> Messages { get; set; }
    }
}
