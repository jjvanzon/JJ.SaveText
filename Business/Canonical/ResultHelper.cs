using System;
using JetBrains.Annotations;
using JJ.Data.Canonical;
using JJ.Framework.Business;
using JJ.Framework.Collections;
using JJ.Framework.Exceptions.Basic;

namespace JJ.Business.Canonical
{
	[PublicAPI]
	public static class ResultHelper
	{
		// Canonical

		public static void Assert(IResultDto result)
		{
			if (result == null) throw new NullException(() => result);

			// ReSharper disable once InvertIf
			if (!result.Successful)
			{
				string formattedMessages = FormatMessages(result);
				throw new Exception(formattedMessages);
			}
		}

		public static string FormatMessages(IResultDto result)
		{
			if (result == null) throw new NullException(() => result);

			string formattedMessages = MessageHelper.FormatMessages(result.Messages);
			return formattedMessages;
		}

		public static IResultDto Combine(params IResultDto[] results)
		{
			if (results == null) throw new NullException(() => results);

			IResultDto result = new VoidResultDto
			{
				Successful = true
			};

			foreach (IResultDto result2 in results)
			{
				result.Successful &= result2.Successful;
				result.Messages.AddRange(result2.Messages);
			}

			return result;
		}

		// Business

		public static VoidResult Successful { get; } = CreateSuccessfulVoidResult();

		private static VoidResult CreateSuccessfulVoidResult() => new VoidResult { Successful = true };

		public static string FormatMessages(IResult result)
		{
			if (result == null) throw new ArgumentNullException(nameof(result));

			string formattedMessages = MessageHelper.FormatMessages(result.Messages);
			return formattedMessages;
		}

		public static IResult Combine(params IResult[] results)
		{
			if (results == null) throw new NullException(() => results);

			IResult result = new VoidResult
			{
				Successful = true
			};

			foreach (IResult result2 in results)
			{
				result.Successful &= result2.Successful;
				result.Messages.AddRange(result2.Messages);
			}

			return result;
		}
	}
}