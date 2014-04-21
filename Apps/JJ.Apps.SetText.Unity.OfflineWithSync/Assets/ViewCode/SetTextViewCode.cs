using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Xml;
using System.ServiceModel;
using System.Net;
using System.Threading;
using JJ.Framework.Persistence;
using JJ.Framework.Persistence.Memory;
using JJ.Framework.Persistence.Xml;
using JJ.Models.Canonical;
using JJ.Models.SetText;
using JJ.Models.SetText.Persistence.Memory.Mapping;
using JJ.Models.SetText.Persistence.Xml.Mapping;
using JJ.Models.SetText.Persistence.Repositories;
using JJ.Models.SetText.Persistence.RepositoryInterfaces;
using JJ.Business.SetText;
using JJ.Apps.SetText.Presenters;
using JJ.Apps.SetText.PresenterInterfaces;
using JJ.Apps.SetText.ViewModels;
using JJ.Apps.SetText.Resources;
using JJ.Apps.SetText.AppService.Interface;
using System.Globalization;

public class SetTextViewCode : MonoBehaviour
{
	private SetTextWithSyncViewModel _viewModel;
	private System.Threading.Timer _timerSynchronization;

	//private int _checkServiceIsAvailableCounter;
	//private string _httpStatus;

	// Use this for initialization
	void Start ()
	{
	}
	
	// Update is called once per frame
	void Update ()
	{
	}

	void OnGUI()
	{
		// Don't know how to do it properly in Unity.
		if (CultureInfo.CurrentUICulture.Name == "en-US") 
		{
			System.Threading.Thread.CurrentThread.CurrentUICulture = CultureInfo.GetCultureInfo("nl-NL");
			System.Threading.Thread.CurrentThread.CurrentCulture = CultureInfo.GetCultureInfo("nl-NL");
		}

		if (_viewModel == null)
		{
			Show ();
		}

		InitializeTimerSynchronization ();

		int y = 50;

		GUI.Label (new Rect (10, y, 200, 20), Titles.SetText);
		y += 30;

		GUI.Label (new Rect (10, y, 200, 20), Labels.Text);
		y += 30;

		_viewModel.Text = GUI.TextField (new Rect (10, 110, 200, 200), _viewModel.Text ?? "");
		y += 210;

		if (GUI.Button (new Rect (10, y, 200, 20), Titles.SetText)) 
		{
			Save();
		}
		y += 30;

		if (_viewModel.TextWasSavedMessageVisible) 
		{
			GUI.Label (new Rect (10, y, 200, 20), Messages.Saved);
			y += 30;
		}

		foreach (var validationMessage in _viewModel.ValidationMessages) 
		{
			GUI.Label (new Rect (10, y, 200, 20), validationMessage.Text);
			y += 30;
		}

		if (_viewModel.SyncSuccessfulMessageVisible) 
		{
			GUI.Label (new Rect (10, 350, 200, 20), Messages.SynchronizedWithServer);
			y += 30;
		}
		
		foreach (var validationMessage in _viewModel.SyncValidationMessages) 
		{
			GUI.Label (new Rect (10, y, 200, 20), validationMessage.Text);
			y += 30;
		}

		//if (_checkServiceIsAvailableCounter > 0)
		//{
		//	GUI.Label (new Rect (10, y, 400, 50), String.Format ("Check service is available counter: {0}.", _checkServiceIsAvailableCounter));
		//	y += 60;
		//}

		//if (_checkServiceIsAvailableCounter > 0)
		//{
		//	GUI.Label (new Rect (10, y, 400, 20), String.Format ("Last HTTP status: {0}.", _httpStatus));
		//	y += 30;
		//}
	}

	private void Show()
	{
		using (IContext context = CreateContext()) 
		{
			IEntityRepository entityRepository = new EntityRepository(context);
			var presenter = new SetTextWithSyncPresenter(entityRepository);
			_viewModel = presenter.Show ();
		}
	}

	private void Save()
	{
		using (IContext context = CreateContext()) 
		{
			IEntityRepository entityRepository = new EntityRepository(context);
			var presenter = new SetTextWithSyncPresenter(entityRepository);
			_viewModel = presenter.Save (_viewModel);
		}
	}

	// Synchronization

	private void InitializeTimerSynchronization()
	{
		if (_timerSynchronization == null) 
		{
			_timerSynchronization = new System.Threading.Timer(
				timerSynchronizationCallback, 
				null, 
				0,
				GetSynchronizationTimerIntervalInMilliseconds ());
		}
	}
	private void timerSynchronizationCallback(object state)
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
				var appService = CreateServiceClient();
				_viewModel = appService.Synchronize(_viewModel);
				appService.Close ();
			}
		}
	}

	private bool CheckServiceIsAvailable()
	{
		string url = GetUrl ();
		int timeout = GetCheckServiceAvailabilityTimeoutInMilliseconds();
		
		HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
		request.Method = "GET";
		request.Timeout = timeout;

		try
		{
			HttpWebResponse response = (HttpWebResponse)request.GetResponse();
			//_httpStatus = response.StatusCode.ToString ();
			return response.StatusCode == HttpStatusCode.OK;
		}
		catch (WebException ex)
		{
			//_httpStatus = "EXCEPTION! " + ex.Message;
			return false;
		}
		//finally
		//{
		//	_checkServiceIsAvailableCounter++;
		//}
	}

	private void Async(Action action)
	{
		var thread = new Thread(new ThreadStart(action));
		thread.Start();
	}

	// Helpers

	private IContext CreateContext()
	{
		//IContext context = new MemoryContext("", typeof(Entity).Assembly, typeof(JJ.Models.SetText.Persistence.Memory.Mapping.EntityMapping).Assembly);
		IContext context = new XmlContext("", typeof(Entity).Assembly, typeof(JJ.Models.SetText.Persistence.Xml.Mapping.EntityMapping).Assembly);
		return context;
	}

	private SetTextWithSyncAppServiceClient CreateServiceClient()
	{
		string url = GetUrl ();
		return new SetTextWithSyncAppServiceClient (url);
	}

	private int GetCheckServiceAvailabilityTimeoutInMilliseconds()
	{
		// TODO: Make setting.
		// Note: If you make the time-out too small, Unity seems to not recover from a previous connection failure somehow.
		return 2000;
	}

	private int GetSynchronizationTimerIntervalInMilliseconds()
	{
		// TODO: Make setting.
		return 5000;
	}

	private string GetUrl()
	{
		// TODO: Make setting.
		return "http://localhost:51116/SetTextWithSyncAppService.svc";
	}
}
