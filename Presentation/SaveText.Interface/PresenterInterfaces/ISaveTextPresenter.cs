using JJ.Presentation.SaveText.Interface.ViewModels;

namespace JJ.Presentation.SaveText.Interface.PresenterInterfaces
{
	public interface ISaveTextPresenter
	{
		SaveTextViewModel Show();
		SaveTextViewModel Save(SaveTextViewModel viewModel);
	}
}
