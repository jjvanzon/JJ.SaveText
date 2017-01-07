using JJ.Presentation.SaveText.Mvc.Names;
using JJ.Presentation.SaveText.Mvc.Helpers;
using JJ.Presentation.SaveText.Presenters;
using JJ.Presentation.SaveText.Interface.ViewModels;
using JJ.Framework.Data;
using JJ.Data.Canonical;
using JJ.Data.SaveText.DefaultRepositories.RepositoryInterfaces;
using System.Web.Mvc;

namespace JJ.Presentation.SaveText.Mvc.Controllers
{
    public class SaveTextController : Controller
    {
        public ActionResult Index()
        {
            SaveTextViewModel viewModel;
            if (TempData.ContainsKey(TempDataKeys.ViewModel))
            {
                viewModel = (SaveTextViewModel)TempData[TempDataKeys.ViewModel];
            }
            else
            {
                using (IContext ormContext = PersistenceHelper.CreateContext())
                {
                    SaveTextPresenter presenter = CreatePresenter(ormContext);
                    viewModel = presenter.Show();
                }
            }

            foreach (Message validationMessage in viewModel.ValidationMessages)
            {
                ModelState.AddModelError(validationMessage.PropertyKey, validationMessage.Text);
            }

            return View(viewModel);
        }

        [HttpPost]
        public ActionResult Index(SaveTextViewModel viewModel)
        {
            using (IContext ormContext = PersistenceHelper.CreateContext())
            {
                SaveTextPresenter presenter = CreatePresenter(ormContext);
                SaveTextViewModel viewModel2 = presenter.Save(viewModel);
                TempData[TempDataKeys.ViewModel] = viewModel2;
                return RedirectToAction(ActionNames.Index);
            }
        }

        private SaveTextPresenter CreatePresenter(IContext context)
        {
            IEntityRepository entityRepository = PersistenceHelper.CreateRepository<IEntityRepository>(context);
            SaveTextPresenter presenter = new SaveTextPresenter(entityRepository);
            return presenter;
        }
    }
}