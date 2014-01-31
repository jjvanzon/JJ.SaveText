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
    internal static class RepositoryFactory
    {
        public static IEntityRepository CreateEntityRepository()
        {
            PersistenceConfiguration configuration = ContextHelper.GetPersistenceConfiguration();

            Assembly xmlPersistenceAssembly = typeof(JJ.Models.SetText.Persistence.Xml.EntityRepository).Assembly;
            string xmlPersistenceAssemblyName = xmlPersistenceAssembly.GetName().Name;

            if (configuration.ContextType == xmlPersistenceAssemblyName)
            {
                Type repositoryType = ReflectionHelper.GetImplementation<IEntityRepository>(xmlPersistenceAssembly);
                string location = HostingEnvironment.MapPath("~/" + configuration.Location);
                return (IEntityRepository)Activator.CreateInstance(repositoryType, location);
            }
            else
            {
                IContext context = ContextHelper.CreateContextFromConfiguration();

                // TODO: You will never get the derived repositories here.
                return new EntityRepository(context);
            }
        }

        /*public static void DisposeEntityRepository(IEntityRepository repository)
        {
            if (repository == null) throw new ArgumentNullException("repository");
            //if (repository is 

            //Type type = repository.GetType();
        }*/
    }
}