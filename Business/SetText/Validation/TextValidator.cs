using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using JJ.Framework.Validation;
using JJ.Data.SaveText;
using JJ.Business.SaveText.Resources;

namespace JJ.Business.SaveText.Validation
{
    internal class TextValidator : FluentValidator_WithoutConstructorArgumentNullCheck<string>
    {
        public TextValidator(string value)
            : base(value)
        { }

        protected override void Execute()
        {
            // Make sure you get the right property key.
            string Text = Object;

            For(() => Text, PropertyDisplayNames.Text)
                .NotNullOrWhiteSpace();
        }
    }
}
