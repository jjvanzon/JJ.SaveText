using System;
using System.Collections.Generic;
using JJ.Framework.Exceptions;
using JJ.Framework.PlatformCompatibility;

namespace JJ.Business.Canonical
{
	public static class MessageHelper
	{
		// DTO

		public static string FormatMessages(IList<string> messages)
		{
			if (messages == null) throw new NullException(() => messages);

			string formattedMessages = String_PlatformSupport.Join(Environment.NewLine, messages);
			return formattedMessages;
		}

		// Business

		public static string FormatMessages(IEnumerable<string> messages)
		{
			if (messages == null) throw new NullException(() => messages);

			string formattedMessages = String_PlatformSupport.Join(Environment.NewLine, messages);
			return formattedMessages;
		}
	}
}