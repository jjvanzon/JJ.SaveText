using System;
using System.Collections.Generic;
using System.Linq;
using JJ.Framework.Validation;
using JJ.Data.Canonical;
using JJ.Framework.Collections;
using JJ.Framework.Exceptions.Basic;

namespace JJ.Business.Canonical
{
	public static class ValidationToCanonicalExtensions
	{
		public static VoidResultDto ToCanonical(this IValidator validator)
		{
			if (validator == null) throw new NullException(() => validator);

			var result = new VoidResultDto
			{
				Successful = validator.IsValid,
				Messages = validator.Messages.ToList()
			};

			return result;
		}

		/// <summary>
		/// Mind that destResult.Successful should be set to true,
		/// if it is ever te be set to true.
		/// </summary>
		public static void ToCanonical(this IEnumerable<IValidator> sourceValidators, IResultDto destResultDto)
		{
			// ReSharper disable once JoinNullCheckWithUsage
			if (sourceValidators == null) throw new NullException(() => sourceValidators);
			if (destResultDto == null) throw new ArgumentNullException(nameof(destResultDto));

			// Prevent multiple enumeration.
			sourceValidators = sourceValidators.ToArray();

			destResultDto.Successful &= sourceValidators.All(x => x.IsValid);

			destResultDto.Messages = destResultDto.Messages ?? new List<string>();

			destResultDto.Messages.AddRange(sourceValidators.SelectMany(x => x.Messages));
		}

		public static VoidResultDto ToCanonical(this IEnumerable<IValidator> validators)
		{
			var result = new VoidResultDto { Successful = true, Messages = new List<string>() };

			ToCanonical(validators, result);

			return result;
		}
	}
}
