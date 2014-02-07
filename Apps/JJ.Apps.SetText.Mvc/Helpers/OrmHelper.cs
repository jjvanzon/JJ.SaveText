using JJ.Framework.Configuration;
using JJ.Framework.Persistence;
using JJ.Framework.Reflection;
using JJ.Models.SetText.Persistence.RepositoryInterfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Web;
using JJ.Models.SetText.Persistence.Repositories;
using System.Web.Hosting;

namespace JJ.Apps.SetText.Mvc.Helpers
{
    internal static class OrmHelper
    {
        public static TRepositoryInterface CreateRepository<TRepositoryInterface>(IContext context)
        {
            return RepositoryFactory.CreateRepositoryFromConfiguration<TRepositoryInterface>(context);
        }

        public static IContext CreateContext()
        {
            PersistenceConfiguration persistenceConfiguration = PersistenceHelper.GetPersistenceConfiguration();

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
                persistenceConfiguration.MappingAssembly);

            return context;
        }
    }
}