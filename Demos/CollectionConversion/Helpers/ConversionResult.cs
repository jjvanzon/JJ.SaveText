using JJ.Framework.Exceptions;
using System.Collections.Generic;

namespace JJ.Demos.CollectionConversion.Helpers
{
	internal class ConversionResult
	{
		public IList<Entity> InsertedEntities { get; }
		public IList<Entity> UpdatedEntities { get; }
		public IList<Entity> DeletedEntities { get; }
		public IList<Entity> UnmodifiedEntities { get; }

		public ConversionResult(
			IList<Entity> insertedEntities,
			IList<Entity> updatedEntities,
			IList<Entity> deletedEntities,
			IList<Entity> unmodifiedEntities)
		{
			if (insertedEntities == null) throw new NullException(() => insertedEntities);
			if (updatedEntities == null) throw new NullException(() => updatedEntities);
			if (deletedEntities == null) throw new NullException(() => deletedEntities);
			if (unmodifiedEntities == null) throw new NullException(() => unmodifiedEntities);

			InsertedEntities = insertedEntities;
			UpdatedEntities = updatedEntities;
			DeletedEntities = deletedEntities;
			UnmodifiedEntities = unmodifiedEntities;
		}
	}
}