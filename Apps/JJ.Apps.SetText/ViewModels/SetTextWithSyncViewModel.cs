using JJ.Models.Canonical;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;

namespace JJ.Apps.SetText.ViewModels
{
    [DataContract]
    public class SetTextWithSyncViewModel
    {
        [DataMember]
        public string Text { get; set; }

        [DataMember]
        public IList<ValidationMessage> ValidationMessages { get; set; }

        [DataMember]
        public bool TextWasSavedMessageVisible { get; set; }

        /// <summary>
        /// Signal to the presentation platform that a pending synchronization should be executed,
        /// as soon as network is available.
        /// </summary>
        [DataMember]
        public bool TextWasSavedButNotYetSynchronized { get; set; }

        [DataMember]
        public bool SyncSuccessfulMessageVisible { get; set; }

        [DataMember]
        public List<ValidationMessage> SyncValidationMessages { get; set; }
    }
}
