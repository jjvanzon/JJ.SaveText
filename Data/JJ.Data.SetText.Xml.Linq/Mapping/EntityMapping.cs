using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using JJ.Framework.Data.Xml.Linq;

namespace JJ.Data.SetText.Xml.Linq.Mapping
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
