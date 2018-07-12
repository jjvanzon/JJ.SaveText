﻿using System;
using System.Text;
using System.Windows.Forms;
using JJ.Data.SaveText.DefaultRepositories.RepositoryInterfaces;
using JJ.Framework.Data;
using JJ.Presentation.SaveText.Interface.PresenterInterfaces;
using JJ.Presentation.SaveText.Interface.ViewModels;
using JJ.Presentation.SaveText.Presenters;
using JJ.Presentation.SaveText.Resources;

namespace JJ.Presentation.SaveText.WinForms.Offline
{
    internal partial class MainForm : Form
    {
        private readonly ISaveTextPresenter _presenter;
        private SaveTextViewModel _viewModel;

        public MainForm()
        {
            InitializeComponent();

            IContext context = CreateContext();
            _presenter = CreatePresenter(context);

            SetTitlesAndLabels();

            Show();
        }

        private void buttonSave_Click(object sender, EventArgs e) => Save();

        private void textBoxText_TextChanged(object sender, EventArgs e) => _viewModel.Text = textBoxText.Text;

        private new void Show()
        {
            _viewModel = _presenter.Show();
            ApplyViewModel();
        }

        private void Save()
        {
            _viewModel = _presenter.Save(_viewModel);
            ApplyViewModel();
        }

        private void ApplyViewModel()
        {
            textBoxText.Text = _viewModel.Text;

            var sb = new StringBuilder();
            if (_viewModel.TextWasSavedMessageVisible)
            {
                sb.AppendLine(Messages.Saved);
            }

            foreach (string message in _viewModel.ValidationMessages)
            {
                sb.AppendLine(message);
            }

            labelValidationMessages.Text = sb.ToString();
        }

        private void SetTitlesAndLabels() => buttonSave.Text = Titles.SaveText;

        private ISaveTextPresenter CreatePresenter(IContext context)
        {
            var repository = RepositoryFactory.CreateRepositoryFromConfiguration<IEntityRepository>(context);
            return new SaveTextPresenter(repository);
        }

        private IContext CreateContext()
        {
            IContext context = ContextFactory.CreateContextFromConfiguration();
            return context;
        }
    }
}