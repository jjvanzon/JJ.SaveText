﻿using JJ.Apps.SetText.AppService.Interface;
using JJ.Apps.SetText.Resources;
using JJ.Apps.SetText.ViewModels;
using JJ.Framework.Configuration;
using JJ.Models.Canonical;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace JJ.Apps.SetText.WinForms.Online
{
    public partial class MainForm : Form
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
            using (SetTextAppServiceClient service = CreateAppServiceClient())
            {
                _viewModel = service.Show();
                ApplyViewModel();
            }
        }

        private void Save()
        {
            using (SetTextAppServiceClient service = CreateAppServiceClient())
            {
                _viewModel = service.Save(_viewModel);
                ApplyViewModel();
            }
        }

        private void SetTitlesAndLabels()
        {
            buttonSave.Text = Titles.SetText;
        }

        private void ApplyViewModel()
        {
            textBoxText.Text = _viewModel.Text;

            var sb = new StringBuilder();
            if (_viewModel.TextWasSavedMessageVisible)
            {
                sb.AppendLine(Messages.Saved);
            }

            foreach (ValidationMessage validationMessage in _viewModel.ValidationMessages)
            {
                sb.AppendLine(validationMessage.Text);
            }

            labelValidationMessages.Text = sb.ToString();
        }

        private SetTextAppServiceClient CreateAppServiceClient()
        {
            string url = AppSettings<IAppSettings>.Get(x => x.AppServiceUrl);
            return new SetTextAppServiceClient(url);
        }
    }
}