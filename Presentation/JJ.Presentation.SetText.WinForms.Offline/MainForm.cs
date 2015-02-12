using JJ.Presentation.SetText.Interface.PresenterInterfaces;
using JJ.Presentation.SetText.Presenters;
using JJ.Presentation.SetText.Resources;
using JJ.Presentation.SetText.Interface.ViewModels;
using JJ.Framework.Persistence;
using JJ.Business.CanonicalModel;
using JJ.Persistence.SetText.DefaultRepositories.RepositoryInterfaces;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace JJ.Presentation.SetText.WinForms.Offline
{
    internal partial class MainForm : Form
    {
        private IContext _context;
        private ISetTextPresenter _presenter;
        private SetTextViewModel _viewModel;

        public MainForm()
        {
            InitializeComponent();

            _context = CreateContext();
            _presenter = CreatePresenter(_context);

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

            foreach (ValidationMessage validationMessage in _viewModel.ValidationMessages)
            {
                sb.AppendLine(validationMessage.Text);
            }

            labelValidationMessages.Text = sb.ToString();
        }

        private void SetTitlesAndLabels()
        {
            buttonSave.Text = Titles.SetText;
        }

        private ISetTextPresenter CreatePresenter(IContext context)
        {
            IEntityRepository repository = RepositoryFactory.CreateRepositoryFromConfiguration<IEntityRepository>(context);
            return new SetTextPresenter(repository);
        }

        private IContext CreateContext()
        {
            IContext context = ContextFactory.CreateContextFromConfiguration();
            return context;
        }
    }
}
