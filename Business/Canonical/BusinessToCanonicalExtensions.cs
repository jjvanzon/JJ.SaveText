
using JetBrains.Annotations;
using JJ.Data.Canonical;
using JJ.Framework.Business;
using JJ.Framework.Exceptions.Basic;

namespace JJ.Business.Canonical
{
    [PublicAPI]
    public static class BusinessToCanonicalExtensions
    {
        public static VoidResultDto ToCanonical(this VoidResult source)
        {
            if (source == null) throw new NullException(() => source);

            var dest = new VoidResultDto
            {
                Successful = source.Successful,
                Messages = source.Messages
            };

            return dest;
        }

        public static ResultDto<T> ToCanonical<T>(this Result<T> source)
        {
            if (source == null) throw new NullException(() => source);

            var dest = new ResultDto<T>
            {
                Successful = source.Successful,
                Messages = source.Messages,
                Data = source.Data
            };

            return dest;
        }
    }
}
