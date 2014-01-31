using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using JJ.Models.Canonical;
using JJ.Models.SetText;

namespace JJ.Apps.SetText.ViewModels.Helpers
{
    internal static class EntityExtensions_ToViewModel
    {
        public static SetTextViewModel ToSetTextViewModel(this Entity entity)
        {
            return new SetTextViewModel
            {
                Text = entity.Text,
                ValidationMessages = new List<ValidationMessage>(),
                TextWasSavedMessageVisible = false
            };
        }
    }
}
