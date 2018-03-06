using System.Collections.Generic;
using JJ.Data.SaveText.DefaultRepositories.RepositoryInterfaces;
using JJ.Presentation.SaveText.Interface.ViewModels;
using JJ.Presentation.SaveText.Helpers;
using JJ.Business.SaveText;
using Canonical = JJ.Data.Canonical;
using JJ.Data.Canonical;
using JJ.Presentation.SaveText.Interface.PresenterInterfaces;
using JJ.Framework.Exceptions;
using JJ.Framework.Exceptions.Basic;

namespace JJ.Presentation.SaveText.Presenters
{
	public class SaveTextPresenter : ISaveTextPresenter
	{
		private IEntityRepository _entityRepository;
		private TextSaver _textSetter;

		public SaveTextPresenter(IEntityRepository entityRepository)
		{
			if (entityRepository == null) throw new NullException(() => entityRepository);

			_entityRepository = entityRepository;
			_textSetter = new TextSaver(entityRepository);
		}

		public SaveTextViewModel Show()
		{
			return CreateViewModel();
		}

		public SaveTextViewModel Save(SaveTextViewModel viewModel)
		{
			viewModel.NullCoallesce();

			VoidResultDto result = _textSetter.SaveText(viewModel.Text);
			if (result.Successful)
			{
				_entityRepository.Commit();
				SaveTextViewModel viewModel2 = CreateViewModel();
				viewModel2.TextWasSavedMessageVisible = true;
				return viewModel2;
			}
			else
			{
				SaveTextViewModel viewModel2 = CreateViewModel();
				viewModel2.ValidationMessages = result.Messages;
				viewModel2.TextWasSavedMessageVisible = false;
				viewModel2.Text = viewModel.Text; // Keep entered value.
				return viewModel2;
			}
		}

		private SaveTextViewModel CreateViewModel()
		{
			string text = _textSetter.GetText();
			var viewModel = new SaveTextViewModel
			{
				Text = text,
				ValidationMessages = new List<string>()
			};
			return viewModel;
		}
	}
}
