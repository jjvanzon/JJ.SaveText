﻿using FluentNHibernate.Mapping;

namespace JJ.Data.SaveText.NHibernate
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
