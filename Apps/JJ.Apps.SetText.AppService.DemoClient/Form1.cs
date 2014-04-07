using JJ.Apps.SetText.AppService.DemoClient.SetTextAppService;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace JJ.Apps.SetText.AppService.DemoClient
{
    public partial class Form1 : Form
    {
        private SetTextViewModel _viewModel;

        public Form1()
        {
            InitializeComponent();

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

        private void Show()
        {
            using (var service = new SetTextAppServiceClient())
            {
                _viewModel = service.Show();
                ApplyViewModel();
            }
        }

        private void Save()
        {
            using (var service = new SetTextAppServiceClient())
            {
                _viewModel = service.Save(_viewModel);
                ApplyViewModel();
            }
        }

        private void ApplyViewModel()
        {
            textBoxText.Text = _viewModel.Text;

            var sb = new StringBuilder();
            if (_viewModel.TextWasSavedMessageVisible)
            {
                sb.AppendLine("Saved!");
            }

            foreach (ValidationMessage validationMessage in _viewModel.ValidationMessages)
            {
                sb.AppendLine(validationMessage.Text);
            }

            labelValidationMessages.Text = sb.ToString();
        }
    }
}
