using JJ.Business.SetText;
using JJ.Models.SetText;
using JJ.Models.SetText.Persistence.RepositoryInterfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JJ.Apps.SetText.ViewModels.Helpers
{
    internal static class SetTextViewModelExtensions_ToEntity
    {
        private const int ENTITY_ID = 1;

        public static Entity ToEntity(this SetTextViewModel viewModel, TextSetter textSetter)
        {
            if (viewModel == null) { throw new ArgumentNullException("viewModel"); }
            if (textSetter == null) { throw new ArgumentNullException("textSetter"); }

            Entity entity = textSetter.GetEntity();
            entity.Text = viewModel.Text;

            return entity;
        }
    }
}
