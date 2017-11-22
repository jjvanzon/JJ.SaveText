using JJ.Framework.Data.Xml.Linq;

namespace JJ.Data.SaveText.Xml.Linq.Mapping
{
	public class EntityMapping : XmlMapping<Entity>
	{
		public EntityMapping()
		{
			IdentityPropertyName = "ID";
			IdentityType = IdentityType.AutoIncrement;
		}
	}
}
