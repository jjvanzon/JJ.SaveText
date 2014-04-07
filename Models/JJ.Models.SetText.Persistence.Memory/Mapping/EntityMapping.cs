using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using JJ.Framework.Persistence.Memory;

namespace JJ.Models.SetText.Persistence.Memory.Mapping
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
