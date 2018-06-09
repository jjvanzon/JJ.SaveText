using System.Collections.Generic;
using JJ.Framework.Exceptions.Basic;
using JJ.Presentation.SaveText.Interface.ViewModels;

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