using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using JJ.Framework.Persistence.Xml;

namespace JJ.Models.SetText.Xml.Mapping
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
