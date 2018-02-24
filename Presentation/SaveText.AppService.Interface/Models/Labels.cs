using System.Runtime.Serialization;

namespace JJ.Presentation.SaveText.AppService.Interface.Models
{
	[DataContract]
	public class Labels
	{
		[DataMember]
		public string Text { get; set; }
	}
}
