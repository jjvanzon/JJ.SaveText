using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace JJ.Business.SetText.Helpers
{
    internal static class ValidationMessageExtensions
    {
        public static JJ.Models.Canonical.ValidationMessage ToCanonical(this JJ.Framework.Validation.ValidationMessage sourceEntity)
        {
            return new Models.Canonical.ValidationMessage
            {
                PropertyKey = sourceEntity.PropertyKey,
                Text = sourceEntity.Text
            };
        }
    }
}
