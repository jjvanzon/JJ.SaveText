using JJ.Framework.Data;
using System.Web.Hosting;

namespace JJ.Presentation.SaveText.Mvc.Helpers
{
	internal static class PersistenceHelper
	{
		public static TRepositoryInterface CreateRepository<TRepositoryInterface>(IContext context)
		{
			return RepositoryFactory.CreateRepositoryFromConfiguration<TRepositoryInterface>(context);
		}

		public static IContext CreateContext()
		{
			// The only reason for all this code is to relate the XML persistence location
			// to the web root, which is not the same as the current directory.

			PersistenceConfiguration persistenceConfiguration = PersistenceConfigurationHelper.GetPersistenceConfiguration();

			string location = persistenceConfiguration.Location;
			bool isXmlContext = persistenceConfiguration.ContextType == "Xml";
			if (isXmlContext)
			{
				location = HostingEnvironment.MapPath("~/" + location);
			}

			IContext context = ContextFactory.CreateContext(
				persistenceConfiguration.ContextType,
				location,
				persistenceConfiguration.ModelAssembly,
				persistenceConfiguration.MappingAssembly,
				persistenceConfiguration.Dialect);

			return context;
		}
	}
}