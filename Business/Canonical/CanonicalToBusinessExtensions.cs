using System.Collections.Generic;
using JetBrains.Annotations;
using JJ.Framework.Business;
using JJ.Data.Canonical;
using JJ.Framework.Exceptions;

namespace JJ.Business.Canonical
{
    public static class CanonicalToBusinessExtensions
    {
        public static VoidResult ToBusiness([NotNull] this VoidResultDto source)
        {
            if (source == null) throw new NullException(() => source);

            var dest = new VoidResult
            {
                Successful = source.Successful,
            };

            if (source.Messages != null)
            {
                dest.Messages = source.Messages.ToBusiness();
            }

            return dest;
        }

        public static Messages ToBusiness(this IList<MessageDto> sourceCollection)
        {
            var destCollection = new Messages();

            foreach (MessageDto sourceMessage in sourceCollection)
            {
                Message destMessage = ToBusiness(sourceMessage);
                destCollection.Add(destMessage);
            }

            return destCollection;
        }

        public static Message ToBusiness([NotNull] this MessageDto source)
        {
            if (source == null) throw new NullException(() => source);

            var dest = new Message(source.Key, source.Text);

            return dest;
        }
    }
}
