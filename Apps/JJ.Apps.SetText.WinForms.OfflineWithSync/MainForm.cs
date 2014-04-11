using JJ.Apps.SetText.ViewModels;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using JJ.Apps.SetText.PresenterInterfaces;
using JJ.Apps.SetText.Presenters;
using JJ.Models.Canonical;
using JJ.Framework.Persistence;
using JJ.Framework.Persistence.Xml;
using JJ.Models.SetText;
using JJ.Models.SetText.Persistence.RepositoryInterfaces;
using JJ.Framework.Configuration;
using JJ.Apps.SetText.AppService.Interface;
using JJ.Apps.SetText.AppService.Interface.Clients;

namespace JJ.Apps.SetText.WinForms.OfflineWithSync
{
    public partial class MainForm : Form
    {
        private IContext _context;
        private ISetTextWithSyncPresenter _presenter;
        private SetTextWithSyncViewModel _viewModel;
        private ISetTextWithSyncPresenter _appService;

        public MainForm()
        {
            InitializeComponent();

            _context = CreateContext();
            _presenter = CreatePresenter(_context);
            _appService = CreateAppServiceClient();

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

        private void buttonSynchronize_Click(object sender, EventArgs e)
        {
            Synchronize();
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

        private void Synchronize()
        {
            _viewModel = _appService.Synchronize(_viewModel);
            ApplyViewModel();
        }

        private void ApplyViewModel()
        {
            textBoxText.Text = _viewModel.Text;

            var sb = new StringBuilder();

            if (_viewModel.TextWasSavedMessageVisible)
            {
                sb.AppendLine("Saved!");
            }

            if (_viewModel.SyncSuccessfulMessageVisible)
            {
                sb.AppendLine("Synchronized with server!");
            }

            foreach (ValidationMessage validationMessage in _viewModel.ValidationMessages)
            {
                sb.AppendLine(validationMessage.Text);
            }

            foreach (ValidationMessage validationMessage in _viewModel.SyncValidationMessages)
            {
                sb.AppendLine(validationMessage.Text);
            }

            if (_viewModel.TextWasSavedButNotYetSynchronized)
            {
                sb.AppendLine("Synchronization pending.");
            }

            labelValidationMessages.Text = sb.ToString();
        }

        private ISetTextWithSyncPresenter CreatePresenter(IContext context)
        {
            IEntityRepository repository = RepositoryFactory.CreateRepositoryFromConfiguration<IEntityRepository>(context);
            return new SetTextWithSyncPresenter(repository);
        }

        private IContext CreateContext()
        {
            IContext context = ContextFactory.CreateContextFromConfiguration();
            return context;
        }

        private ISetTextWithSyncPresenter CreateAppServiceClient()
        {
            string url = AppSettings<IAppSettings>.Get(x => x.AppServiceUrl);
            return new SetTextWithSyncAppServiceClient(url);
        }
    }
}
