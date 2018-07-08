using System.Collections.Generic;
using JJ.Framework.Exceptions.Basic;
// ReSharper disable UnusedAutoPropertyAccessor.Global

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
            InsertedEntities = insertedEntities ?? throw new NullException(() => insertedEntities);
            UpdatedEntities = updatedEntities ?? throw new NullException(() => updatedEntities);
            DeletedEntities = deletedEntities ?? throw new NullException(() => deletedEntities);
            UnmodifiedEntities = unmodifiedEntities ?? throw new NullException(() => unmodifiedEntities);
        }
    }
}