using System;
using System.Collections.Generic;
using System.Linq;

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
            // ReSharper disable once JoinNullCheckWithUsage
            if (validators == null) throw new NullException(() => validators);
            if (destResult == null) throw new ArgumentNullException(nameof(destResult));

            // Prevent multiple enumeration.
            validators = validators.ToArray();

            destResult.Successful &= validators.All(x => x.IsValid);

            destResult.Messages.AddRange(validators.SelectMany(x => x.Messages));
        }

        public static VoidResult ToResult(this IEnumerable<IValidator> validators) => validators.ToCanonical().ToBusiness();
    }
}