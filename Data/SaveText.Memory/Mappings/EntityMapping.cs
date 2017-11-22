using JJ.Framework.Data.Memory;

namespace JJ.Data.SaveText.Memory.Mappings
{
	public class EntityMapping : MemoryMapping<Entity>
	{
		public EntityMapping()
		{
			IdentityPropertyName = "ID";
			IdentityType = IdentityType.AutoIncrement;
		}
	}
}
