using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace JJ.Presentation.SetText.Helpers
{
    internal static class ValidationMessageExtensions
    {
        public static JJ.Business.CanonicalModel.ValidationMessage ToCanonical(this JJ.Framework.Validation.ValidationMessage sourceEntity)
        {
            return new JJ.Business.CanonicalModel.ValidationMessage
            {
                PropertyKey = sourceEntity.PropertyKey,
                Text = sourceEntity.Text
            };
        }
    }
}
