using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using JJ.Models.Canonical;

namespace JJ.Apps.SetText.ViewModels
{
    public class SetTextViewModel
    {
        public string Text { get; set; }

        public IList<ValidationMessage> ValidationMessages { get; set; }

        public bool TextWasSavedMessageVisible { get; set; }
    }
}
