using JJ.Presentation.SetText.WinForms.Online.GeneratedServiceClient.ResourceAppService;
using JJ.Presentation.SetText.WinForms.Online.GeneratedServiceClient.SetTextAppService;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace JJ.Presentation.SetText.WinForms.Online.GeneratedServiceClient
{
    internal partial class MainForm : Form
    {
        private string _cultureName = Thread.CurrentThread.CurrentUICulture.Name;

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
            using (var service = new SetTextAppServiceClient())
            {
                _viewModel = service.Show(_cultureName);
                ApplyViewModel();
            }
        }

        private void Save()
        {
            using (var service = new SetTextAppServiceClient())
            {
                _viewModel = service.Save(_viewModel, _cultureName);
                ApplyViewModel();
            }
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

            foreach (ValidationMessage validationMessage in _viewModel.ValidationMessages)
            {
                sb.AppendLine(validationMessage.Text);
            }

            labelValidationMessages.Text = sb.ToString();
        }
    }
}