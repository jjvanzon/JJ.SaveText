using JJ.Apps.SetText.Interface.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Canonical = JJ.Models.Canonical;

namespace JJ.Apps.SetText.Helpers
{
    internal static class ViewModelExtensions_NullCoallesce
    {
        public static void NullCoallesce(this SetTextViewModel viewModel)
        {
            if (viewModel == null) throw new ArgumentNullException("viewModel");
            viewModel.ValidationMessages = viewModel.ValidationMessages ?? new List<Canonical.ValidationMessage>();
        }
    }
}
