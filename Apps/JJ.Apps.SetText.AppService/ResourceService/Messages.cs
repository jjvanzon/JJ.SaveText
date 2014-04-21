using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Web;

namespace JJ.Apps.SetText.AppService.Models
{
    [DataContract]
    public class Messages
    {
        [DataMember]
        public string Saved { get; set; }
    }
}