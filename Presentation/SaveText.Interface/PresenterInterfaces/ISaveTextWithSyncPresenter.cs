using JJ.Presentation.SaveText.Interface.ViewModels;

namespace JJ.Presentation.SaveText.Interface.PresenterInterfaces
{
	public interface ISaveTextWithSyncPresenter
	{
		SaveTextViewModel Show();
		SaveTextViewModel Save(SaveTextViewModel viewModel);
		SaveTextViewModel Synchronize(SaveTextViewModel viewModel);
	}
}
