using System.Collections.Generic;
using System.Linq;
using JetBrains.Annotations;
using JJ.Framework.Exceptions;
using JJ.Framework.Business;
using JJ.Data.Canonical;

namespace JJ.Business.Canonical
{
    public static class BusinessToCanonicalExtensions
    {
        public static VoidResultDto ToCanonical([NotNull] this VoidResult source)
        {
            if (source == null) throw new NullException(() => source);

            var dest = new VoidResultDto
            {
                Successful = source.Successful,
                Messages = source.Messages.ToCanonical()
            };

            return dest;
        }

        public static ResultDto<T> ToCanonical<T>([NotNull] this Result<T> source)
        {
            if (source == null) throw new NullException(() => source);

            var dest = new ResultDto<T>
            {
                Successful = source.Successful,
                Messages = source.Messages.ToCanonical(),
                Data = source.Data,
            };

            return dest;
        }

        public static IList<MessageDto> ToCanonical(this Messages sourceCollection)
        {
            IList<MessageDto> destCollection = sourceCollection.Select(ToCanonical).ToList();
            return destCollection;
        }

        public static MessageDto ToCanonical(this Message source)
        {
            if (source == null) throw new NullException(() => source);

            var dest = new MessageDto
            {
                Key = source.Key,
                Text = source.Text
            };

            return dest;
        }
    }
}
