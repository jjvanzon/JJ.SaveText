using JJ.Presentation.SaveText.AppService.Interface;
using JJ.Presentation.SaveText.Interface.PresenterInterfaces;
using JJ.Presentation.SaveText.Interface.ViewModels;
using System.ServiceModel;

namespace JJ.Presentation.SaveText.AppService.Client.Wcf
{
	// ISaveTextAppService differs from ISaveTextPresenter only in the additional parameters for the service methods.
	// You can pass those to the constructor of the client, expose the presenter interface, and pass the extra parameters to the Channel.

	public class SaveTextAppServiceClient : ClientBase<ISaveTextAppService>, ISaveTextPresenter
	{
		private string _cultureName;

		public SaveTextAppServiceClient(string url, string cultureName)
			: base(new BasicHttpBinding(), new EndpointAddress(url))
		    => _cultureName = cultureName;

	    public SaveTextViewModel Show() => Channel.Show(_cultureName);

	    public SaveTextViewModel Save(SaveTextViewModel viewModel) => Channel.Save(viewModel, _cultureName);
	}
}
