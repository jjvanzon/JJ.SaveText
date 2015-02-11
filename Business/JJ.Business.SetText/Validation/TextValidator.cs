using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using JJ.Framework.Validation;
using JJ.Persistence.SetText;
using JJ.Business.SetText.Resources;

namespace JJ.Business.SetText.Validation
{
    internal class TextValidator : FluentValidator<string>
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
