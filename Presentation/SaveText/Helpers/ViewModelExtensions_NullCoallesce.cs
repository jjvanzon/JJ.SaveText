using JJ.Presentation.SaveText.Interface.ViewModels;
using JJ.Framework.Exceptions;
using System.Collections.Generic;

namespace JJ.Presentation.SaveText.Helpers
{
    internal static class ViewModelExtensions_NullCoallesce
    {
        public static void NullCoallesce(this SaveTextViewModel viewModel)
        {
            if (viewModel == null) throw new NullException(() => viewModel);
            viewModel.ValidationMessages = viewModel.ValidationMessages ?? new List<string>();
        }
    }
}
