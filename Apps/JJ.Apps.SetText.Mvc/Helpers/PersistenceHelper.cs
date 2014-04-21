﻿using JJ.Framework.Configuration;
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
                persistenceConfiguration.MappingAssembly);

            return context;
        }
    }
}