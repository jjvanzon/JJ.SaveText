using System.Collections.Generic;
using JJ.Business.SaveText;
using JJ.Data.Canonical;
using JJ.Data.SaveText.DefaultRepositories.RepositoryInterfaces;
using JJ.Framework.Exceptions.Basic;
using JJ.Presentation.SaveText.Helpers;
using JJ.Presentation.SaveText.Interface.PresenterInterfaces;
using JJ.Presentation.SaveText.Interface.ViewModels;

namespace JJ.Presentation.SaveText.Presenters
{
    public class SaveTextPresenter : ISaveTextPresenter
    {
        private readonly IEntityRepository _entityRepository;
        private readonly TextSaver _textSetter;

        public SaveTextPresenter(IEntityRepository entityRepository)
        {
            _entityRepository = entityRepository ?? throw new NullException(() => entityRepository);
            _textSetter = new TextSaver(entityRepository);
        }

        public SaveTextViewModel Show() => CreateViewModel();

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