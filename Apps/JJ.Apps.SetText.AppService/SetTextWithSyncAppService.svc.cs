﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;
using Canonical = JJ.Models.Canonical;
using JJ.Apps.SetText.ViewModels;
using JJ.Framework.Persistence;
using JJ.Apps.SetText.AppService.Helpers;
using JJ.Apps.SetText.Presenters;
using JJ.Models.SetText.Persistence.RepositoryInterfaces;
using JJ.Apps.SetText.PresenterInterfaces;
using JJ.Apps.SetText.AppService.Interface;

namespace JJ.Apps.SetText.AppService
{
    public class SetTextWithSyncAppService : ISetTextWithSyncAppService
    {
        public SetTextWithSyncViewModel Show()
        {
            using (IContext context = PersistenceHelper.CreateContext())
            {
                IEntityRepository entityRepository = PersistenceHelper.CreateRepository<IEntityRepository>(context);
                var presenter = new SetTextWithSyncPresenter(entityRepository);
                var viewModel = presenter.Show();
                return viewModel;
            }
        }

        public SetTextWithSyncViewModel Save(SetTextWithSyncViewModel viewModel)
        {
            using (IContext context = PersistenceHelper.CreateContext())
            {
                IEntityRepository entityRepository = PersistenceHelper.CreateRepository<IEntityRepository>(context);
                var presenter = new SetTextWithSyncPresenter(entityRepository);
                var viewModel2 = presenter.Save(viewModel);
                return viewModel2;
            }
        }

        public SetTextWithSyncViewModel Synchronize(SetTextWithSyncViewModel viewModel)
        {
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