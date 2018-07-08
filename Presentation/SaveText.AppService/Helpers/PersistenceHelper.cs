using JJ.Framework.Data;

namespace JJ.Presentation.SaveText.AppService.Helpers
{
    internal static class PersistenceHelper
    {
        public static TRepositoryInterface CreateRepository<TRepositoryInterface>(IContext context)
            => RepositoryFactory.CreateRepositoryFromConfiguration<TRepositoryInterface>(context);

        public static IContext CreateContext() => ContextFactory.CreateContextFromConfiguration();
    }
}