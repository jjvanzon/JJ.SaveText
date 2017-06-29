using System.Collections.Generic;
using JetBrains.Annotations;

namespace JJ.Data.Canonical
{
    public class ResultDto<T> : IResultDto
    {
        public bool Successful { get; set; }
        public IList<MessageDto> Messages { get; set; }

        [CanBeNull]
        public T Data { get; set; }
    }
}
