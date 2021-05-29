using System.Diagnostics;
using System.Runtime.Serialization;
using JJ.Data.Canonical.Helpers;

namespace JJ.Data.Canonical
{
    [DataContract]
    [DebuggerDisplay("{" + nameof(DebuggerDisplay) + "}")]
    public class IDAndName
    {
        [DataMember]
        public int ID { get; set; }

        [DataMember]
        public string Name { get; set; }

        private string DebuggerDisplay => DebuggerDisplayFormatter.GetDebuggerDisplay(this);
    }
}
