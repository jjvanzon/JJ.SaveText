﻿using JJ.Framework.Data.Xml;

namespace JJ.Data.SaveText.Xml.Mapping
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
