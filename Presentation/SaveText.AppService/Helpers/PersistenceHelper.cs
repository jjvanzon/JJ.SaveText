using JJ.Framework.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace JJ.Presentation.SaveText.AppService.Helpers
{
    internal static class PersistenceHelper
    {
        public static TRepositoryInterface CreateRepository<TRepositoryInterface>(IContext context)
        {
            return RepositoryFactory.CreateRepositoryFromConfiguration<TRepositoryInterface>(context);
        }

        public static IContext CreateContext()
        {
            PersistenceConfiguration persistenceConfiguration = PersistenceConfigurationHelper.GetPersistenceConfiguration();
            return ContextFactory.CreateContextFromConfiguration();
        }
    }
}
