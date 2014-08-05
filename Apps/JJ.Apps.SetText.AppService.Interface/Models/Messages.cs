using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Web;

namespace JJ.Apps.SetText.AppService.Interface.Models
{
    [DataContract]
    public class Messages
    {
        [DataMember]
        public string Saved { get; set; }

        [DataMember]
        public string ServiceUnavailable { get; set; }

        [DataMember]
        public string SynchronizationPending { get; set; }

        [DataMember]
        public string SynchronizedWithServer { get; set; }
    }
}