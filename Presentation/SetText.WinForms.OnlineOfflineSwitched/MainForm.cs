using JJ.Presentation.SaveText.Interface.ViewModels;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using JJ.Presentation.SaveText.Interface.PresenterInterfaces;
using JJ.Presentation.SaveText.Presenters;
using JJ.Data.Canonical;
using JJ.Framework.Data;
using JJ.Framework.Data.Xml;
using JJ.Data.SaveText;
using JJ.Data.SaveText.DefaultRepositories.RepositoryInterfaces;
using JJ.Framework.Configuration;
using JJ.Presentation.SaveText.AppService.Client.Wcf;
using JJ.Presentation.SaveText.Resources;
using JJ.Framework.Common;
using System.Globalization;
using Message = JJ.Data.Canonical.Message;

namespace JJ.Presentation.SaveText.WinForms.OnlineOfflineSwitched
{
    internal partial class MainForm : Form
    {
        private IContext _context;
        private SaveTextWithSyncPresenter _presenter;
        private SaveTextAppServiceClient _service;
        private SaveTextViewModel _viewModel;

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

            foreach (Message validationMessage in _viewModel.ValidationMessages)
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
            buttonSave.Text = Titles.SaveText;
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
            using (SaveTextWithSyncAppServiceClient appServiceWithSync = CreateAppServiceClientWithSync())
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

        private SaveTextWithSyncAppServiceClient CreateAppServiceClientWithSync()
        {
            string url = AppSettings<IAppSettings>.Get(x => x.SaveTextWithSyncAppServiceUrl);
            string cultureName = CultureInfo.CurrentUICulture.Name;
            return new SaveTextWithSyncAppServiceClient(url, cultureName);
        }

        private SaveTextAppServiceClient CreateAppServiceClient()
        {
            string url = AppSettings<IAppSettings>.Get(x => x.SaveTextAppServiceUrl);
            string cultureName = CultureInfo.CurrentUICulture.Name;
            return new SaveTextAppServiceClient(url, cultureName);
        }

        private SaveTextWithSyncPresenter CreatePresenter(IContext context)
        {
            IEntityRepository repository = RepositoryFactory.CreateRepositoryFromConfiguration<IEntityRepository>(context);
            return new SaveTextWithSyncPresenter(repository);
        }

        private IContext CreateContext()
        {
            IContext context = ContextFactory.CreateContextFromConfiguration();
            return context;
        }
    }
}
