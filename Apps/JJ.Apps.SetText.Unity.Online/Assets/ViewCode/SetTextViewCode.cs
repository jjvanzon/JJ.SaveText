// Online

using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Xml;
using System.ServiceModel;
using JJ.Framework.Common;
using JJ.Models.Canonical;
using JJ.Models.SetText;
using JJ.Business.SetText;
using JJ.Business.SetText.Resources;
using JJ.Apps.SetText.ViewModels;
using JJ.Apps.SetText.Resources;
using JJ.Apps.SetText.PresenterInterfaces;
using JJ.Apps.SetText.AppService.Interface;
using System.Globalization;
using System.Threading;

public class SetTextViewCode : MonoBehaviour
{
	private SetTextViewModel _viewModel;
	private Exception _lastException;
	private string _debugInfo;

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
		try
		{
			if (_lastException != null)
			{
				string exceptionMessage = ExceptionHelper.FormatExceptionWithInnerExceptions(_lastException, includeStackTrace: true);
				GUI.Label (new Rect(0, 0, 580, 3000), exceptionMessage);
				if (GUI.Button (new Rect(580, 0, 100, 20), "Clear"))
				{
					_lastException = null;
				}
				return;
			}

			EnsureCultureIsInitialized ();

			if (_viewModel == null)
			{
				Show ();
			}

			GUI.Label (new Rect (10, 50, 200, 20), Titles.SetText);

			GUI.Label (new Rect (10, 80, 200, 20), Labels.Text);

			_viewModel.Text = GUI.TextField (new Rect (10, 110, 200, 200), _viewModel.Text ?? "");

			if (GUI.Button (new Rect (10, 320, 200, 20), Titles.SetText)) 
			{
				Save();
			}

			if (_viewModel.TextWasSavedMessageVisible) 
			{
				GUI.Label (new Rect (10, 350, 200, 20), Messages.Saved);
			}

			int y = 360;
			foreach (var validationMessage in _viewModel.ValidationMessages) 
			{
				GUI.Label (new Rect (10, y, 200, 20), validationMessage.Text);
				y += 30;
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
		using (var appService = CreateServiceClient())
		{
			_viewModel = appService.Show();
		}
	}

	private void Save()
	{
		using (var appService = CreateServiceClient())
		{
			_viewModel = appService.Save(_viewModel);
		}
	}

	// Culture

	private bool _cultureIsInitialized = false;

	private void EnsureCultureIsInitialized()
	{
		EnsureCultureIsInitialized_ByAssigningResourceCulture ();
	}

	private void EnsureCultureIsInitialized_ByAssigningResourceCulture()
	{
		if (!_cultureIsInitialized)
		{
			_cultureIsInitialized = true;
			
			CultureInfo cultureInfo = GetCultureInfo ("nl-NL");
			Labels.Culture = cultureInfo;
			Titles.Culture = cultureInfo;
			Messages.Culture = cultureInfo;
			PropertyDisplayNames.Culture = cultureInfo;
			JJ.Framework.Validation.Resources.Messages.Culture = cultureInfo;
		}
	}
	
	private void EnsureCultureIsInitialized_ByAssigningThreadCulture()
	{
		if (!_cultureIsInitialized)
		{
			_cultureIsInitialized = true;

			CultureInfo cultureInfo = GetCultureInfo ("nl-NL");
			Thread.CurrentThread.CurrentUICulture = cultureInfo;
			Thread.CurrentThread.CurrentCulture = cultureInfo;
		}
	}

	private CultureInfo GetCultureInfo(string cultureName)
	{
		return new CultureInfo(cultureName);
	}
	
	// Service Client
	
	private SetTextAppServiceClient CreateServiceClient()
	{
		string url = "http://localhost:51116/SetTextAppService.svc";
		return new SetTextAppServiceClient (url);
	}
}