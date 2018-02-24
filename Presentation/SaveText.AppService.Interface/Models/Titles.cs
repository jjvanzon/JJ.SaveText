using System.Runtime.Serialization;

namespace JJ.Presentation.SaveText.AppService.Interface.Models
{
	[DataContract]
	public class Titles
	{
		[DataMember]
		public string GoOffline { get; set; }

		[DataMember]
		public string GoOnline { get; set; }

		[DataMember]
		public string SaveText { get; set; }

		[DataMember]
		public string Synchronize { get; set; }

		[DataMember]
		public string TryAgain { get; set; }
	}
}