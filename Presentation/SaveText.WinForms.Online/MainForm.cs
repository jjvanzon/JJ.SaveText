using JJ.Presentation.SaveText.AppService.Client.Wcf;
using JJ.Presentation.SaveText.Interface.ViewModels;
using JJ.Framework.Configuration;
using System;
using System.Globalization;
using System.Text;
using System.Windows.Forms;

namespace JJ.Presentation.SaveText.WinForms.Online
{
	internal partial class MainForm : Form
	{
		private SaveTextViewModel _viewModel;

		public MainForm()
		{
			InitializeComponent();

			SetTitlesAndLabels();

			Show();
		}

		private void buttonSave_Click(object sender, EventArgs e) => Save();

	    private void textBoxText_TextChanged(object sender, EventArgs e) => _viewModel.Text = textBoxText.Text;

	    private new void Show()
		{
			using (SaveTextAppServiceClient service = CreateAppServiceClient())
			{
				_viewModel = service.Show();
				ApplyViewModel();
			}
		}

		private void Save()
		{
			using (SaveTextAppServiceClient service = CreateAppServiceClient())
			{
				_viewModel = service.Save(_viewModel);
				ApplyViewModel();
			}
		}

		private void SetTitlesAndLabels() => buttonSave.Text = ResourceHelper.Titles.SaveText;

	    private void ApplyViewModel()
		{
			textBoxText.Text = _viewModel.Text;

			var sb = new StringBuilder();
			if (_viewModel.TextWasSavedMessageVisible)
			{
				sb.AppendLine(ResourceHelper.Messages.Saved);
			}

			foreach (string message in _viewModel.ValidationMessages)
			{
				sb.AppendLine(message);
			}

			labelValidationMessages.Text = sb.ToString();
		}

		private SaveTextAppServiceClient CreateAppServiceClient()
		{
			string url = AppSettingsReader<IAppSettings>.Get(x => x.SaveTextAppService);
			string cultureName = CultureInfo.CurrentUICulture.Name;
			return new SaveTextAppServiceClient(url, cultureName);
		}
	}
}