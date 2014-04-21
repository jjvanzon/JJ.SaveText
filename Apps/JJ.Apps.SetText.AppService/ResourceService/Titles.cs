using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Web;

namespace JJ.Apps.SetText.AppService.Models
{
    [DataContract]
    public class Titles
    {
        [DataMember]
        public string SetText { get; set; }
    }
}