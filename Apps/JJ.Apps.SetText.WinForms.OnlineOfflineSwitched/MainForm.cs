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
using JJ.Apps.SetText.Resources;

namespace JJ.Apps.SetText.WinForms.OnlineOfflineSwitched
{
    public partial class MainForm : Form
    {
        private IContext _context;
        private ISetTextWithSyncPresenter _presenter;
        private SetTextWithSyncViewModel _viewModel;

        public MainForm()
        {
            InitializeComponent();

            SetTitlesAndLabels();

            GoOffline();

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

        private void buttonSwitchBetweenOnlineAndOffline_Click(object sender, EventArgs e)
        {
            SwitchBetweenOnlineAndOffline();
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
            _viewModel = _presenter.Synchronize(_viewModel);
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

            if (_viewModel.SyncSuccessfulMessageVisible)
            {
                sb.AppendLine(Messages.SynchronizedWithServer);
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
                sb.AppendLine(Messages.SynchronizationPending);
            }

            labelValidationMessages.Text = sb.ToString();
        }

        private void SetTitlesAndLabels()
        {
            buttonSave.Text = Titles.SetText;
        }

        // Online / Offline

        private bool IsOnline
        {
            get { return _presenter is SetTextWithSyncAppServiceClient; }
        }

        private void SwitchBetweenOnlineAndOffline()
        {
            if (IsOnline)
            {
                GoOffline();
            }
            else
            {
                GoOnline();
            }
        }

        private void ApplyIsOnline()
        {
            if (IsOnline)
            {
                buttonSwitchBetweenOnlineAndOffline.Text = Titles.GoOffline;
            }
            else
            {
                buttonSwitchBetweenOnlineAndOffline.Text = Titles.GoOnline;
            }
        }

        private void GoOnline()
        {
            if (_context != null)
            {
                _context.Dispose();
                _context = null;
            }

            _presenter = CreateOnlinePresenter();

            Synchronize();

            ApplyIsOnline();
        }

        private void GoOffline()
        {
            _context = CreateContext();
            _presenter = CreateOfflinePresenter(_context);

            ApplyIsOnline();
        }

        private ISetTextWithSyncPresenter CreateOnlinePresenter()
        {
            string url = AppSettings<IAppSettings>.Get(x => x.AppServiceUrl);
            return new SetTextWithSyncAppServiceClient(url);
        }

        private ISetTextWithSyncPresenter CreateOfflinePresenter(IContext context)
        {
            IEntityRepository repository = RepositoryFactory.CreateRepositoryFromConfiguration<IEntityRepository>(context);
            return new SetTextWithSyncPresenter(repository);
        }

        private IContext CreateContext()
        {
            IContext context = ContextFactory.CreateContextFromConfiguration();
            return context;
        }
    }
}
