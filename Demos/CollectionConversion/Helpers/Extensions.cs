namespace JJ.Demos.CollectionConversion.Helpers
{
    internal static class Extensions
    {
        public static void UnlinkRelatedEntities(this Entity entity)
        {
            // Stub. Do nothing.
        }

        public static Entity ToEntity(this ViewModel viewModel, IRepository repository)
        {
            Entity entity = repository.TryGet(viewModel.ID);
            if (entity == null)
            {
                entity = repository.Create();
            }
            entity.Name = viewModel.Name;
            return entity;
        }
    }
}
