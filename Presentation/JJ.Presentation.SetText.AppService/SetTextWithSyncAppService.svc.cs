﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;
using Canonical = JJ.Business.CanonicalModel;
using JJ.Presentation.SetText.Interface.ViewModels;
using JJ.Framework.Data;
using JJ.Presentation.SetText.Presenters;
using JJ.Data.SetText.DefaultRepositories.RepositoryInterfaces;
using JJ.Presentation.SetText.Interface.PresenterInterfaces;
using JJ.Presentation.SetText.AppService.Interface;
using JJ.Presentation.SetText.AppService.Helpers;

namespace JJ.Presentation.SetText.AppService
{
    public class SetTextWithSyncAppService : ISetTextWithSyncAppService
    {
        public SetTextViewModel Show(string cultureName)
        {
            ServiceHelper.SetCulture(cultureName);

            using (IContext context = PersistenceHelper.CreateContext())
            {
                IEntityRepository entityRepository = PersistenceHelper.CreateRepository<IEntityRepository>(context);
                var presenter = new SetTextWithSyncPresenter(entityRepository);
                var viewModel = presenter.Show();
                return viewModel;
            }
        }

        public SetTextViewModel Save(SetTextViewModel viewModel, string cultureName)
        {
            ServiceHelper.SetCulture(cultureName);

            using (IContext context = PersistenceHelper.CreateContext())
            {
                IEntityRepository entityRepository = PersistenceHelper.CreateRepository<IEntityRepository>(context);
                var presenter = new SetTextWithSyncPresenter(entityRepository);
                var viewModel2 = presenter.Save(viewModel);
                return viewModel2;
            }
        }

        public SetTextViewModel Synchronize(SetTextViewModel viewModel, string cultureName)
        {
            ServiceHelper.SetCulture(cultureName);

            using (IContext context = PersistenceHelper.CreateContext())
            {
                IEntityRepository entityRepository = PersistenceHelper.CreateRepository<IEntityRepository>(context);
                var presenter = new SetTextWithSyncPresenter(entityRepository);
                var viewModel2 = presenter.Synchronize(viewModel);
                return viewModel2;
            }
        }
    }
}