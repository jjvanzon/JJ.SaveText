using System;
using System.Collections.Generic;
using System.Linq;
using JJ.Data.Canonical;
using JJ.Framework.Business;
using JJ.Framework.Collections;
using JJ.Framework.Exceptions;
using JJ.Framework.Exceptions.Basic;

namespace JJ.Business.Canonical
{
	// ReSharper disable once InconsistentNaming
	public static class ResultExtensions
	{
		// DTO

		public static void Combine(this IResultDto destResult, IResultDto sourceResult)
		{
			if (destResult == null) throw new NullException(() => destResult);
			if (sourceResult == null) throw new NullException(() => sourceResult);

			destResult.Successful &= sourceResult.Successful;
			destResult.Messages = destResult.Messages ?? new List<string>();
			destResult.Messages.AddRange(sourceResult.Messages);
		}

		public static void Assert(this IResultDto result)
		{
			if (result == null) throw new NullException(() => result);

			ResultHelper.Assert(result);
		}

		// Business

		/// <param name="sourceResult">
		/// Initialize sourceResult.Successful = false to begin with,
		/// this method will always keep it set to false.
		/// </param>
		public static void Combine(this IResult destResult, IResult sourceResult, string messagePrefix = null)
		{
			if (destResult == null) throw new NullException(() => destResult);
			if (sourceResult == null) throw new NullException(() => sourceResult);

			destResult.Successful &= sourceResult.Successful;

			if (!string.IsNullOrEmpty(messagePrefix))
			{
				destResult.Messages.AddRange(sourceResult.Messages.Select(x => messagePrefix + x));
			}
			else
			{
				destResult.Messages.AddRange(sourceResult.Messages);
			}
		}

		public static void Assert(this IResult result)
		{
			if (result == null) throw new NullException(() => result);

			if (result == null) throw new NullException(() => result);

			// ReSharper disable once InvertIf
			if (!result.Successful)
			{
				string formattedMessages = ResultHelper.FormatMessages(result);
				throw new Exception(formattedMessages);
			}
		}
	}
}
