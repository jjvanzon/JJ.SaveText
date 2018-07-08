using JJ.Framework.Validation;
using JJ.Business.SaveText.Resources;

namespace JJ.Business.SaveText.Validation
{
	internal class TextValidator : VersatileValidator
	{
		public TextValidator(string text) => For(text, PropertyDisplayNames.Text).NotNullOrWhiteSpace();
	}
}
