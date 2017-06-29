using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using JetBrains.Annotations;
using JJ.Framework.Business;
using JJ.Data.Canonical;
using JJ.Framework.Exceptions;
using JJ.Framework.Validation;

namespace JJ.Business.Canonical
{
    // ReSharper disable once InconsistentNaming
    public static class ValidationToBusinessExtensions
    {
        public static VoidResult ToResult(this IValidator validator) => validator.ToCanonical().ToBusiness();

        /// <summary> Mind that destResult.Successful should be set to true, if it is ever te be set to true. </summary>
        public static void ToResult([NotNull] this IEnumerable<IValidator> validators, [NotNull] IResult destResult)
        {
            // ReSharper disable once JoinNullCheckWithUsage
            if (validators == null) throw new NullException(() => validators);
            if (destResult == null) throw new ArgumentNullException(nameof(destResult));

            // Prevent multiple enumeration.
            validators = validators.ToArray();

            destResult.Successful &= validators.All(x => x.IsValid);

            Messages messages2 = validators.SelectMany(x => x.ValidationMessages).ToCanonical().ToBusiness();
            destResult.Messages.AddRange(messages2);
        }

        public static VoidResult ToResult([NotNull] this IEnumerable<IValidator> validators) => validators.ToCanonical().ToBusiness();

        public static Messages ToBusiness(this ValidationMessages validationMessages) => validationMessages.ToCanonical().ToBusiness();
    }
}