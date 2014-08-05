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
using JJ.Apps.SetText.AppService.Interface.WcfClient;
using JJ.Apps.SetText.Resources;
using JJ.Framework.Common;
using System.Globalization;

namespace JJ.Apps.SetText.WinForms.OnlineOfflineSwitched
{
    public partial class MainForm : Form
    {
        private IContext _context;
        private SetTextWithSyncPresenter _presenter;
        private SetTextAppServiceClient _service;
        private SetTextViewModel _viewModel;

        public MainForm()
        {
            InitializeComponent();

            SetTitlesAndLabels();

            GoOffline();

            Show();
        }

        ~MainForm()
        {
            if (_service != null)
            {
                IDisposable disposable = _service as IDisposable;
                disposable.Dispose();
            }
        }

        // Events

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

        // Actions

        private new void Show()
        {
            if (IsOnline)
            {
                _viewModel = _service.Show();
            }
            else
            {
                _viewModel = _presenter.Show();
            }
            ApplyViewModel();
        }

        private void Save()
        {
            if (IsOnline)
            {
                _viewModel = _service.Save(_viewModel);
            }
            else
            {
                _viewModel = _presenter.Save(_viewModel);
            }
            ApplyViewModel();
        }

        // Apply to GUI

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
            get 
            {
                if (_service != null)
                {
                    return true;
                }

                if (_presenter != null)
                {
                    return false;
                }

                throw new Exception("Both _service and _presenter are null.");
            }
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
            // Synchronize
            using (SetTextWithSyncAppServiceClient appServiceWithSync = CreateAppServiceClientWithSync())
            {
                _viewModel = appServiceWithSync.Synchronize(_viewModel);
                ApplyViewModel();
            }

            // Clean up presenter
            if (_context != null)
            {
                _context.Dispose();
                _context = null;
            }
            _presenter = null;

            // Create app service client
            _service = CreateAppServiceClient();

            // Apply to GUI
            ApplyIsOnline();
        }

        private void GoOffline()
        {
            // Clean up app service client
            if (_service != null)
            {
                _service.Close();
                _service = null;
            }

            // Create presenter
            _context = CreateContext();
            _presenter = CreatePresenter(_context);

            // Apply to GUI
            ApplyIsOnline();
        }

        private SetTextWithSyncAppServiceClient CreateAppServiceClientWithSync()
        {
            string url = AppSettings<IAppSettings>.Get(x => x.SetTextWithSyncAppServiceUrl);
            string cultureName = CultureInfo.CurrentUICulture.Name;
            return new SetTextWithSyncAppServiceClient(url, cultureName);
        }

        private SetTextAppServiceClient CreateAppServiceClient()
        {
            string url = AppSettings<IAppSettings>.Get(x => x.SetTextAppServiceUrl);
            string cultureName = CultureInfo.CurrentUICulture.Name;
            return new SetTextAppServiceClient(url, cultureName);
        }

        private SetTextWithSyncPresenter CreatePresenter(IContext context)
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
