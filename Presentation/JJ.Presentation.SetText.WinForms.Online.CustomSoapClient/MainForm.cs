using JJ.Presentation.SetText.AppService.Client.Custom;
using JJ.Presentation.SetText.AppService.Interface.Models;
using JJ.Presentation.SetText.Interface.ViewModels;
using JJ.Framework.Configuration;
using JJ.Business.CanonicalModel;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Message = JJ.Business.CanonicalModel.Message;

namespace JJ.Presentation.SetText.WinForms.Online
{
    internal partial class MainForm : Form
    {
        private SetTextViewModel _viewModel;

        public MainForm()
        {
            InitializeComponent();

            SetTitlesAndLabels();

            Show();
        }

        private void buttonSave_Click(object sender, EventArgs e)
        {
            Save();
        }

        private void textBoxText_TextChanged(object sender, EventArgs e)
        {
            _viewModel.Text = textBoxText.Text;
        }

        private new void Show()
        {
            SetTextAppServiceClient service = CreateAppServiceClient();
            _viewModel = service.Show();
            ApplyViewModel();
        }

        private void Save()
        {
            SetTextAppServiceClient service = CreateAppServiceClient();
            _viewModel = service.Save(_viewModel);
            ApplyViewModel();
        }

        private void SetTitlesAndLabels()
        {
            buttonSave.Text = ResourceHelper.Titles.SetText;
        }

        private void ApplyViewModel()
        {
            textBoxText.Text = _viewModel.Text;

            var sb = new StringBuilder();
            if (_viewModel.TextWasSavedMessageVisible)
            {
                sb.AppendLine(ResourceHelper.Messages.Saved);
            }

            foreach (Message validationMessage in _viewModel.ValidationMessages)
            {
                sb.AppendLine(validationMessage.Text);
            }

            labelValidationMessages.Text = sb.ToString();
        }

        private SetTextAppServiceClient CreateAppServiceClient()
        {
            string url = AppSettings<IAppSettings>.Get(x => x.SetTextAppService);
            string cultureName = CultureInfo.CurrentUICulture.Name;
            return new SetTextAppServiceClient(url, cultureName);
        }
    }
}