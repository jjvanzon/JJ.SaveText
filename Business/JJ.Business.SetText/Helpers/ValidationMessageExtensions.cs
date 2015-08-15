﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace JJ.Business.SetText.Helpers
{
    internal static class ValidationMessageExtensions
    {
        public static JJ.Business.CanonicalModel.Message ToCanonical(this JJ.Framework.Validation.ValidationMessage sourceEntity)
        {
            return new JJ.Business.CanonicalModel.Message
            {
                PropertyKey = sourceEntity.PropertyKey,
                Text = sourceEntity.Text
            };
        }
    }
}