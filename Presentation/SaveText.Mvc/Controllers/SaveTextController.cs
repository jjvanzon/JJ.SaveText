using JJ.Presentation.SaveText.Mvc.Names;
using JJ.Presentation.SaveText.Mvc.Helpers;
using JJ.Presentation.SaveText.Presenters;
using JJ.Presentation.SaveText.Interface.ViewModels;
using JJ.Framework.Data;
using JJ.Data.SaveText.DefaultRepositories.RepositoryInterfaces;
using System.Web.Mvc;

namespace JJ.Presentation.SaveText.Mvc.Controllers
{
	public class SaveTextController : Controller
	{
		public ActionResult Index()
		{
			SaveTextViewModel viewModel;
			if (TempData.ContainsKey(nameof(TempDataKeys.ViewModel)))
			{
				viewModel = (SaveTextViewModel)TempData[nameof(TempDataKeys.ViewModel)];
			}
			else
			{
				using (IContext ormContext = PersistenceHelper.CreateContext())
				{
					SaveTextPresenter presenter = CreatePresenter(ormContext);
					viewModel = presenter.Show();
				}
			}

			foreach (string message in viewModel.ValidationMessages)
			{
				ModelState.AddModelError(nameof(message), message);
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
				TempData[nameof(TempDataKeys.ViewModel)] = viewModel2;
				return RedirectToAction(nameof(ActionNames.Index));
			}
		}

		private SaveTextPresenter CreatePresenter(IContext context)
		{
			var entityRepository = PersistenceHelper.CreateRepository<IEntityRepository>(context);
			var presenter = new SaveTextPresenter(entityRepository);
			return presenter;
		}
	}
}