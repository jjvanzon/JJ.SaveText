using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Web;

namespace JJ.Apps.SetText.AppService.Interface.Models
{
    [DataContract]
    public class Titles
    {
        [DataMember]
        public string GoOffline { get; set; }

        [DataMember]
        public string GoOnline { get; set; }

        [DataMember]
        public string SetText { get; set; }

        [DataMember]
        public string Synchronize { get; set; }

        [DataMember]
        public string TryAgain { get; set; }
    }
}