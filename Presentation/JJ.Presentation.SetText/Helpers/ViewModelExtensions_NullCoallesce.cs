﻿using JJ.Presentation.SetText.Interface.ViewModels;
using JJ.Framework.Reflection.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Canonical = JJ.Business.CanonicalModel;

namespace JJ.Presentation.SetText.Helpers
{
    internal static class ViewModelExtensions_NullCoallesce
    {
        public static void NullCoallesce(this SetTextViewModel viewModel)
        {
            if (viewModel == null) throw new NullException(() => viewModel);
            viewModel.ValidationMessages = viewModel.ValidationMessages ?? new List<Canonical.Message>();
        }
    }
}