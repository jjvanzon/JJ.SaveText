using System;
using System.Collections.Generic;
using JetBrains.Annotations;
using JJ.Framework.Exceptions.Basic;

namespace JJ.Business.Canonical
{
	[PublicAPI]
	public static class MessageHelper
	{
		// DTO

		public static string FormatMessages(IList<string> messages)
		{
			if (messages == null) throw new NullException(() => messages);

			string formattedMessages = string.Join(Environment.NewLine, messages);
			return formattedMessages;
		}

		// Business

		public static string FormatMessages(IEnumerable<string> messages)
		{
			if (messages == null) throw new NullException(() => messages);

			string formattedMessages = string.Join(Environment.NewLine, messages);
			return formattedMessages;
		}
	}
}