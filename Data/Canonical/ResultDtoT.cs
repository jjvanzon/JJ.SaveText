using System.Collections.Generic;

namespace JJ.Data.Canonical
{
    public class ResultDto<T> : IResultDto
    {
        public bool Successful { get; set; }
        public IList<string> Messages { get; set; }
        
        public T Data { get; set; }
    }
}
