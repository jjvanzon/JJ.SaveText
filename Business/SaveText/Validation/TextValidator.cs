using JJ.Framework.Validation;
using JJ.Business.SaveText.Resources;

namespace JJ.Business.SaveText.Validation
{
    internal class TextValidator : VersatileValidator_WithoutConstructorArgumentNullCheck<string>
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
