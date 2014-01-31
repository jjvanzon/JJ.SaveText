using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using JJ.Framework.Validation;
using JJ.Models.SetText;
using JJ.Business.SetText.Resources;

namespace JJ.Business.SetText.Validation
{
    public class TextValidator : FluentValidator<string>
    {
        public TextValidator(string value)
            : base(value)
        { }

        protected override void Execute()
        {
            For(() => Object, PropertyDisplayNames.Text)
                .NotNullOrWhiteSpace();
        }
    }
}
