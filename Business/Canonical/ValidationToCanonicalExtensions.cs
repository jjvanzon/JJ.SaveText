using System;
using System.Collections.Generic;
using System.Linq;
using JJ.Framework.Validation;
using JJ.Data.Canonical;
using JJ.Framework.Exceptions;
using JetBrains.Annotations;
using JJ.Framework.Collections;

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
                Messages = validator.ValidationMessages.ToCanonical()
            };

            return result;
        }

        /// <summary>
        /// Mind that destResult.Successful should be set to true,
        /// if it is ever te be set to true.
        /// </summary>
        public static void ToCanonical([NotNull] this IEnumerable<IValidator> sourceValidators, [NotNull] IResultDto destResultDto)
        {
            // ReSharper disable once JoinNullCheckWithUsage
            if (sourceValidators == null) throw new NullException(() => sourceValidators);
            if (destResultDto == null) throw new ArgumentNullException(nameof(destResultDto));

            // Prevent multiple enumeration.
            sourceValidators = sourceValidators.ToArray();

            destResultDto.Successful &= sourceValidators.All(x => x.IsValid);

            destResultDto.Messages = destResultDto.Messages ?? new List<MessageDto>();

            destResultDto.Messages.AddRange(sourceValidators.SelectMany(x => x.ValidationMessages).ToCanonical());
        }

        public static VoidResultDto ToCanonical([NotNull] this IEnumerable<IValidator> validators)
        {
            var result = new VoidResultDto { Successful = true, Messages = new List<MessageDto>() };

            ToCanonical(validators, result);

            return result;
        }

        public static MessageDto ToCanonical([NotNull] this ValidationMessage sourceEntity)
        {
            if (sourceEntity == null) throw new NullException(() => sourceEntity);

            return new MessageDto
            {
                Key = sourceEntity.Key,
                Text = sourceEntity.Text
            };
        }

        public static IList<MessageDto> ToCanonical([NotNull] this IEnumerable<ValidationMessage> sourceList)
        {
            if (sourceList == null) throw new NullException(() => sourceList);

            IList<MessageDto> destList = sourceList.Select(sourceItem => sourceItem.ToCanonical()).ToList();

            return destList;
        }

    }
}
