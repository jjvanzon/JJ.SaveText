﻿using JJ.Framework.Persistence;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace JJ.Presentation.SetText.AppService.Helpers
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