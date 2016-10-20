using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using JJ.Presentation.SaveText.Presenters;
using JJ.Data.Canonical;
using JJ.Framework.Data;
using JJ.Framework.Data.Xml;
using JJ.Data.SaveText;
using JJ.Data.SaveText.DefaultRepositories.RepositoryInterfaces;
using JJ.Framework.Configuration;
using JJ.Presentation.SaveText.AppService.Client.Wcf;
using JJ.Presentation.SaveText.Resources;
using JJ.Presentation.SaveText.Interface.ViewModels;
using System.Net;
using System.Threading;
using System.Globalization;
using JJ.Presentation.SaveText.Interface.PresenterInterfaces;
using Message = JJ.Data.Canonical.Message;

namespace JJ.Presentation.SaveText.WinForms.OfflineWithSync
{
    internal partial class MainForm : Form
    {
        private IContext _context;
        private ISaveTextWithSyncPresenter _presenter;
        private SaveTextViewModel _viewModel;
        private ISaveTextWithSyncPresenter _appService;

        public MainForm()
        {
            InitializeComponent();

            _context = CreateContext();
            _presenter = CreatePresenter(_context);
            _appService = CreateAppServiceClient();

            InitializeTimerConditionalSynchronize();
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

        private ISaveTextWithSyncPresenter CreatePresenter(IContext context)
        {
            IEntityRepository repository = RepositoryFactory.CreateRepositoryFromConfiguration<IEntityRepository>(context);
            return new SaveTextWithSyncPresenter(repository);
        }

        private IContext CreateContext()
        {
            IContext context = ContextFactory.CreateContextFromConfiguration();
            return context;
        }

        private ISaveTextWithSyncPresenter CreateAppServiceClient()
        {
            string url = AppSettings<IAppSettings>.Get(x => x.AppServiceUrl);
            string cultureName = CultureInfo.CurrentUICulture.Name;
            return new SaveTextWithSyncAppServiceClient(url, cultureName);
        }

        // Synchronization

        private void InitializeTimerConditionalSynchronize()
        {
            timerSynchronization.Interval = AppSettings<IAppSettings>.Get(x => x.SynchronizationTimerIntervalInMilliseconds);
        }

        private void timerSynchronization_Tick(object sender, EventArgs e)
        {
            Async(() => ConditionalSynchronize());
        }

        private void ConditionalSynchronize()
        {
            if (_viewModel.TextWasSavedButNotYetSynchronized)
            {
                bool serviceIsAvailable = CheckServiceIsAvailable();
                if (serviceIsAvailable)
                {
                    _viewModel = _appService.Synchronize(_viewModel);

                    OnUiThread(() => ApplyViewModel());
                }
            }
        }

        private bool CheckServiceIsAvailable()
        {
            string url = AppSettings<IAppSettings>.Get(x => x.AppServiceUrl);
            int timeout = AppSettings<IAppSettings>.Get(x => x.CheckServiceAvailabilityTimeoutInMilliseconds);

            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
            request.Method = "GET";
            request.Timeout = timeout;

            try
            {
                HttpWebResponse response = (HttpWebResponse)request.GetResponse();
                return response.StatusCode == HttpStatusCode.OK;
            }
            catch (WebException)
            {
                return false;
            }
        }

        private void Async(Action action)
        {
            var thread = new Thread(new ThreadStart(action));
            thread.Start();
        }

        private void OnUiThread(Action action)
        {
            this.BeginInvoke(action);
        }
    }
}
