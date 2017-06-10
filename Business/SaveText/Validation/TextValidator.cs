using JJ.Framework.Validation;
using JJ.Business.SaveText.Resources;

namespace JJ.Business.SaveText.Validation
{
    internal class TextValidator : VersatileValidator_WithoutConstructorArgumentNullCheck<string>
    {
        public TextValidator(string text)
            : base(text)
        { 
            For(() => text, PropertyDisplayNames.Text).NotNullOrWhiteSpace();
        }
    }
}
