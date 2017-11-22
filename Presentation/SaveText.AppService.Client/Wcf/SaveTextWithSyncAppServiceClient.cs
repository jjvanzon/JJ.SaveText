using JJ.Presentation.SaveText.AppService.Interface;
using JJ.Presentation.SaveText.Interface.PresenterInterfaces;
using JJ.Presentation.SaveText.Interface.ViewModels;
using System.ServiceModel;

namespace JJ.Presentation.SaveText.AppService.Client.Wcf
{
	public class SaveTextWithSyncAppServiceClient : ClientBase<ISaveTextWithSyncAppService>, ISaveTextWithSyncPresenter
	{
		private string _cultureName;

		public SaveTextWithSyncAppServiceClient(string url, string cultureName)
			: base(new BasicHttpBinding(), new EndpointAddress(url))
		{
			_cultureName = cultureName;
		}

		public SaveTextViewModel Show()
		{
			return Channel.Show(_cultureName);
		}

		public SaveTextViewModel Save(SaveTextViewModel viewModel)
		{
			return Channel.Save(viewModel, _cultureName);
		}

		public SaveTextViewModel Synchronize(SaveTextViewModel viewModel)
		{
			return Channel.Synchronize(viewModel, _cultureName);
		}
	}
}
