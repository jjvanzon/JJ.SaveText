using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using JJ.Framework.Data.Memory;

namespace JJ.Data.SetText.Memory.Mappings
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
