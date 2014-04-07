using JJ.Models.Canonical;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace JJ.Apps.SetText.ViewModels
{
    public class SetTextWithSyncViewModel
    {
        public string Text { get; set; }

        public IList<ValidationMessage> ValidationMessages { get; set; }

        public bool TextWasSavedMessageVisible { get; set; }

        /// <summary>
        /// Signal to the presentation platform that a pending synchronization should be executed,
        /// as soon as network is available.
        /// </summary>
        public bool TextWasSavedButNotYetSynchronized { get; set; }

        public bool SyncSuccessfulMessageVisible { get; set; }

        public IList<ValidationMessage> SyncValidationMessages { get; set; }
    }
}
