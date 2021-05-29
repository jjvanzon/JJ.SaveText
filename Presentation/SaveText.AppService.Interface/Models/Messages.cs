using System.Runtime.Serialization;

namespace JJ.Presentation.SaveText.AppService.Interface.Models
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