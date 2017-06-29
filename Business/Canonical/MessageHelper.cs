using System;
using System.Collections.Generic;
using System.Linq;
using JJ.Data.Canonical;
using JJ.Framework.Business;
using JJ.Framework.PlatformCompatibility;
using JJ.Framework.Exceptions;

namespace JJ.Business.Canonical
{
    public static class MessageHelper
    {
        // DTO

        public static string FormatMessages(IList<MessageDto> messages)
        {
            if (messages == null) throw new NullException(() => messages);

            string formattedMessages = String_PlatformSupport.Join(Environment.NewLine, messages.Select(x => x.Text));
            return formattedMessages;
        }

        // Business
        
        public static string FormatMessages(IEnumerable<Message> messages)
        {
            if (messages == null) throw new NullException(() => messages);

            string formattedMessages = String_PlatformSupport.Join(Environment.NewLine, messages.Select(x => x.Text));
            return formattedMessages;
        }
    }
}
