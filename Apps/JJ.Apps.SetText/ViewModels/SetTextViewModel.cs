using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using JJ.Models.Canonical;
using System.Runtime.Serialization;

namespace JJ.Apps.SetText.ViewModels
{
    [DataContract]
    public class SetTextViewModel
    {
        [DataMember]
        public string Text { get; set; }

        [DataMember]
        public List<ValidationMessage> ValidationMessages { get; set; }

        [DataMember]
        public bool TextWasSavedMessageVisible { get; set; }
    }
}
