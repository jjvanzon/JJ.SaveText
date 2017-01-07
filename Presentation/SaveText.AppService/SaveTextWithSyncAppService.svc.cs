using JJ.Presentation.SaveText.Interface.ViewModels;
using JJ.Framework.Data;
using JJ.Presentation.SaveText.Presenters;
using JJ.Data.SaveText.DefaultRepositories.RepositoryInterfaces;
using JJ.Presentation.SaveText.AppService.Interface;
using JJ.Presentation.SaveText.AppService.Helpers;

namespace JJ.Presentation.SaveText.AppService
{
    public class SaveTextWithSyncAppService : ISaveTextWithSyncAppService
    {
        public SaveTextViewModel Show(string cultureName)
        {
            ServiceHelper.SetCulture(cultureName);

            using (IContext context = PersistenceHelper.CreateContext())
            {
                IEntityRepository entityRepository = PersistenceHelper.CreateRepository<IEntityRepository>(context);
                var presenter = new SaveTextWithSyncPresenter(entityRepository);
                var viewModel = presenter.Show();
                return viewModel;
            }
        }

        public SaveTextViewModel Save(SaveTextViewModel viewModel, string cultureName)
        {
            ServiceHelper.SetCulture(cultureName);

            using (IContext context = PersistenceHelper.CreateContext())
            {
                IEntityRepository entityRepository = PersistenceHelper.CreateRepository<IEntityRepository>(context);
                var presenter = new SaveTextWithSyncPresenter(entityRepository);
                var viewModel2 = presenter.Save(viewModel);
                return viewModel2;
            }
        }

        public SaveTextViewModel Synchronize(SaveTextViewModel viewModel, string cultureName)
        {
            ServiceHelper.SetCulture(cultureName);

            using (IContext context = PersistenceHelper.CreateContext())
            {
                IEntityRepository entityRepository = PersistenceHelper.CreateRepository<IEntityRepository>(context);
                var presenter = new SaveTextWithSyncPresenter(entityRepository);
                var viewModel2 = presenter.Synchronize(viewModel);
                return viewModel2;
            }
        }
    }
}
