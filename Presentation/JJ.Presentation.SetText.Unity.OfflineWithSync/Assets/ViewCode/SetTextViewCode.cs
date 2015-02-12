﻿// OfflineWithSync

using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Net;
using System.Threading;
using System.Reflection;
using System.Globalization;
using JJ.Framework.Logging;
using JJ.Framework.PlatformCompatibility;
using JJ.Framework.Persistence;
using JJ.Framework.Persistence.Memory;
using JJ.Framework.Persistence.Xml.Linq;
using JJ.Persistence.SetText;
using JJ.Persistence.SetText.DefaultRepositories.RepositoryInterfaces;
using JJ.Business.CanonicalModel;
using JJ.Business.SetText;
using JJ.Business.SetText.Resources;
using JJ.Presentation.SetText.Interface.ViewModels;
using JJ.Presentation.SetText.Interface.PresenterInterfaces;
using JJ.Presentation.SetText.Presenters;
using JJ.Presentation.SetText.Resources;
using JJ.Presentation.SetText.AppService.Interface;
using JJ.Presentation.SetText.AppService.Client.Custom;

public class SetTextViewCode : MonoBehaviour
{
	private SetTextViewModel _viewModel;

	// TODO: Make settings
	private string _cultureName = "nl-NL";
	private string _url = "http://83.82.26.17:6371/SetTextWithSyncAppService.svc";
	private int _checkServiceAvailabilityTimeoutInMilliseconds = 2000; // Note: If you make the time-out too small, Unity seems to not recover from a previous connection failure somehow.
	private int _syncTimerIntervalInMilliseconds = 5000;

	private Exception _lastException;
	private string _debugInfo;
	
	private int _width = 200;
	private int _lineHeight = 24;
	private int _spacing = 10;
	private int _textBoxHeight = 160;
	private GUIStyle _titleStyle;

	private System.Threading.Timer _syncTimer;

	void Start ()
	{
		_titleStyle = new GUIStyle ();
		_titleStyle.fontStyle = FontStyle.Bold;
		_titleStyle.fontSize = 14;
		_titleStyle.normal.textColor = new Color (255, 255, 255);

		InitializeCulture ();
	}
	
	// Update is called once per frame
	void Update ()
	{
	}

	void OnGUI()
	{
		try
		{
			if (_lastException != null)
			{
				string exceptionMessage = ExceptionHelper.FormatExceptionWithInnerExceptions(_lastException, includeStackTrace: true);
				GUI.Label (new Rect(0, 0, 580, 3000), exceptionMessage);
				if (GUI.Button (new Rect(580, 0, 100, _lineHeight), "Clear"))
				{
					_lastException = null;
				}
				return;
			}

			if (_viewModel == null)
			{
				Show ();
			}

			EnsureSyncTimerIsInitialized ();
			
			int y = _spacing;

			GUI.Label (new Rect (_spacing, y, _width, _lineHeight), Titles.SetText);
			y += _lineHeight;
			y += _spacing;

			GUI.Label (new Rect (_spacing, y, _width, _lineHeight), Labels.Text);
			y += _lineHeight;

			_viewModel.Text = GUI.TextField (new Rect (_spacing, y, _width, _textBoxHeight), _viewModel.Text ?? "");
			y += _textBoxHeight;
			y += _spacing;

			if (GUI.Button (new Rect (_spacing, y, _width, _lineHeight), Titles.SetText)) 
			{
				Save();
			}
			y += _lineHeight;
			y += _spacing;

			if (_viewModel.TextWasSavedMessageVisible) 
			{
				GUI.Label (new Rect (_spacing, y, _width, _lineHeight), Messages.Saved);
				y += _lineHeight;
				y += _spacing;
			}
			
			if (_viewModel.SyncSuccessfulMessageVisible) 
			{
				GUI.Label (new Rect (_spacing, y, _width, _lineHeight), Messages.SynchronizedWithServer);
				y += _lineHeight;
				y += _spacing;
			}

			foreach (var validationMessage in _viewModel.ValidationMessages) 
			{
				GUI.Label (new Rect (_spacing, y, _width, _lineHeight), validationMessage.Text);
				y += _lineHeight;
				y += _spacing;
			}

			if (_viewModel.TextWasSavedButNotYetSynchronized) 
			{
				GUI.Label (new Rect (_spacing, y, _width, _lineHeight), Messages.SynchronizationPending);
				y += _lineHeight;
				y += _spacing;
			}

			//if (!String.IsNullOrEmpty(_debugInfo))
			//{
			//	GUI.Label (new Rect(0, 0, 600, 600), "DebugInfo: " + _debugInfo);
			//}
		}
		catch (Exception ex)
		{
			_lastException = ex;
		}
	}

	private void Show()
	{
		using (IContext context = CreateContext()) 
		{
			IEntityRepository entityRepository = CreateRepository(context);
			var presenter = new SetTextWithSyncPresenter(entityRepository);
			_viewModel = presenter.Show ();
		}
	}

	private void Save()
	{
		using (IContext context = CreateContext()) 
		{
			IEntityRepository entityRepository = CreateRepository(context);
			var presenter = new SetTextWithSyncPresenter(entityRepository);
			_viewModel = presenter.Save (_viewModel);
		}
	}

	// Culture
	
	private void InitializeCulture()
	{
		// Assigning thread culture is not possible on iOS 6.
		// So assign resource cultures instead.

		CultureInfo cultureInfo = CultureInfo_PlatformSafe.GetCultureInfo (_cultureName);
		Labels.Culture = cultureInfo;
		Titles.Culture = cultureInfo;
		Messages.Culture = cultureInfo;
		PropertyDisplayNames.Culture = cultureInfo;
		JJ.Framework.Validation.Resources.Messages.Culture = cultureInfo;
	}

	// Synchronization

	private void EnsureSyncTimerIsInitialized()
	{
		if (_syncTimer == null) 
		{
			_syncTimer = new System.Threading.Timer(
				syncTimerCallback, 
				null, 
				_syncTimerIntervalInMilliseconds,
				_syncTimerIntervalInMilliseconds);

			Debug.Log("_syncTimer is initialized.");
		}
	}

	private void syncTimerCallback(object state)
	{
		Debug.Log("syncTimerCallback");

		Async(() => ConditionalSynchronize());
	}

	private void ConditionalSynchronize()
	{
		if (_viewModel.TextWasSavedButNotYetSynchronized)
		{
			Debug.Log("TextWasSavedButNotYetSynchronized");

			bool serviceIsAvailable = CheckServiceIsAvailable();
			if (serviceIsAvailable)
			{
				Debug.Log("serviceIsAvailable");

				ISetTextWithSyncPresenter appService = CreateServiceClient();
				_viewModel = appService.Synchronize(_viewModel);

				Debug.Log("Synchronized");
			}
		}
	}

	private bool CheckServiceIsAvailable()
	{
		Debug.Log("CheckServiceIsAvailable");

		string url = _url;
		int timeout = _checkServiceAvailabilityTimeoutInMilliseconds;
		
		HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
		request.Method = "GET";
		request.Timeout = timeout;

		try
		{
			HttpWebResponse response = (HttpWebResponse)request.GetResponse();

			Debug.Log(String.Format ("HTTP status: {0}.", response.StatusCode));

			return response.StatusCode == HttpStatusCode.OK;
		}
		catch (WebException ex)
		{
			Debug.Log(String.Format ("EXCEPTION! {0}", ex.Message));

			return false;
		}
	}

	private void Async(Action action)
	{
		var thread = new Thread(new ThreadStart(action));
		thread.Start();
	}

	// Persistence
	
	private IContext CreateContext()
	{
		return CreateXmlContext ();
	}
	
	private IContext CreateMemoryContext()
	{
		Assembly modelAssembly = typeof(Entity).Assembly;
		Assembly mappingAssembly = typeof(JJ.Persistence.SetText.Memory.Mapping.EntityMapping).Assembly;
		return new MemoryContext("", modelAssembly, mappingAssembly);
	}
	
	private IContext CreateXmlContext()
	{
		// Windows Phone will not take the absolute location that Application.persistentDataPath.
		// TODO: Is there a better property to use than Application.persistentDataPath, that will work on all the targeted platforms?
		string folderPath;
		if (Application.platform == RuntimePlatform.WP8Player) 
		{
			folderPath = "";
		}
		else
		{
			folderPath = Application.persistentDataPath;
		}
		
		Assembly modelAssembly = typeof(Entity).Assembly;
		Assembly mappingAssembly = typeof(JJ.Persistence.SetText.Xml.Linq.Mapping.EntityMapping).Assembly;
		return new XmlContext(folderPath, modelAssembly, mappingAssembly);
	}

	private IEntityRepository CreateRepository(IContext context)
	{
		return CreateXmlRepository (context);
	}
	
	private IEntityRepository CreateMemoryRepository(IContext context)
	{
		return new JJ.Persistence.SetText.Memory.Repositories.EntityRepository(context);
	}
	
	private IEntityRepository CreateXmlRepository(IContext context)
	{
		return new JJ.Persistence.SetText.Xml.Linq.Repositories.EntityRepository(context);
	}

	// Helpers

	private ISetTextWithSyncPresenter CreateServiceClient()
	{
		return new SetTextWithSyncAppServiceClient (_url, _cultureName);
	}
}
