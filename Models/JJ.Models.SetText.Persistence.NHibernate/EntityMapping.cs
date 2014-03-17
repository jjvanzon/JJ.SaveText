using FluentNHibernate.Mapping;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace JJ.Models.SetText.Persistence.NHibernate
{
    public class EntityMapping : ClassMap<Entity>
    {
        public EntityMapping()
        {
            Id(x => x.ID).GeneratedBy.Assigned();
            Map(x => x.Text);
        }
    }
}
