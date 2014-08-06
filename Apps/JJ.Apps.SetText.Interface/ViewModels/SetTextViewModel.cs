using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using JJ.Models.Canonical;
using System.Runtime.Serialization;

namespace JJ.Apps.SetText.Interface.ViewModels
{
    [DataContract]
    public class SetTextViewModel
    {
        [DataMember]
        public string Text { get; set; }

        [DataMember]
        public IList<ValidationMessage> ValidationMessages { get; set; }

        [DataMember]
        public bool TextWasSavedMessageVisible { get; set; }

        /// <summary>
        /// Signal to the presentation platform that a pending synchronization should be executed,
        /// as soon as the service is available.
        /// Does not apply to online / offline situations that do not have synchronization.
        /// </summary>
        [DataMember]
        public bool TextWasSavedButNotYetSynchronized { get; set; }

        /// <summary>
        /// Does not apply to online / offline situations that do not have synchronization.
        /// </summary>
        [DataMember]
        public bool SyncSuccessfulMessageVisible { get; set; }
    }
}
