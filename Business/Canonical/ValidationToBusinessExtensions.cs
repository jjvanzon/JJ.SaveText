using System;
using System.Collections.Generic;

using JJ.Framework.Business;
using JJ.Framework.Collections;
using JJ.Framework.Exceptions;
using JJ.Framework.Validation;

namespace JJ.Business.Canonical
{
	// ReSharper disable once InconsistentNaming
	public static class ValidationToBusinessExtensions
	{
		public static VoidResult ToResult(this IValidator validator) => validator.ToCanonical().ToBusiness();

		/// <summary> Mind that destResult.Successful should be set to true, if it is ever te be set to true. </summary>
		public static void ToResult(this IEnumerable<IValidator> validators, IResult destResult)
		{
			if (validators == null) throw new NullException(() => validators);
			if (destResult == null) throw new ArgumentNullException(nameof(destResult));

			validators.ForEach(x => x.ToResult(destResult));
		}

		/// <summary> Mind that destResult.Successful should be set to true, if it is ever te be set to true. </summary>
		public static void ToResult(this IValidator validator, IResult destResult)
		{
			if (validator == null) throw new NullException(() => validator);
			if (destResult == null) throw new ArgumentNullException(nameof(destResult));

			destResult.Successful &= validator.IsValid;
			destResult.Messages.AddRange(validator.Messages);
		}

		public static VoidResult ToResult(this IEnumerable<IValidator> validators) => validators.ToCanonical().ToBusiness();
	}
}