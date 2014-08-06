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
using JJ.Apps.SetText.AppService.Client.Wcf;
using JJ.Apps.SetText.Resources;
using JJ.Apps.SetText.ViewModels;
using System.Net;
using System.Threading;
using System.Globalization;

namespace JJ.Apps.SetText.WinForms.OfflineWithSync
{
    public partial class MainForm : Form
    {
        private IContext _context;
        private ISetTextWithSyncPresenter _presenter;
        private SetTextViewModel _viewModel;
        private ISetTextWithSyncPresenter _appService;

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
            string cultureName = CultureInfo.CurrentUICulture.Name;
            return new SetTextWithSyncAppServiceClient(url, cultureName);
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
