// Online

using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Xml;
using JJ.Framework.Common;
//using JJ.Models.Canonical;
//using JJ.Models.SetText;
//using JJ.Business.SetText;
//using JJ.Business.SetText.Resources;
//using JJ.Apps.SetText.ViewModels;
//using JJ.Apps.SetText.Resources;
//using JJ.Apps.SetText.PresenterInterfaces;
//using JJ.Apps.SetText.AppService.Interface;
using JJ.Apps.SetText.Unity.Online;
using System.Globalization;
using System.Threading;
using System.Net;
//using System.ServiceModel;

public class SetTextViewCode : MonoBehaviour
{
	private SetTextViewModel _viewModel;

	// TODO: Make settings
	private string _url = "http://83.82.26.17:6371/SetTextAppService.svc";
	private string _cultureName = "nl-NL";

	private Exception _lastException;
	private string _debugInfo;
	private Exception _lastWebException;
	private int _attempts;
	private Action _lastAction;
	
	private int _width = 200;
	private int _lineHeight = 24;
	private int _spacing = 10;
	private int _textBoxHeight = 160;
	private GUIStyle _titleStyle;

	private ResourceHelper _resourceHelper;
	
	void Start ()
	{
		_titleStyle = new GUIStyle ();
		_titleStyle.fontStyle = FontStyle.Bold;
		_titleStyle.fontSize = 14;
		_titleStyle.normal.textColor = new Color (255, 255, 255);

		_resourceHelper = new ResourceHelper (_cultureName);
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

			int y = _spacing;

			if (_lastWebException != null)
			{
				string message = String.Format(_resourceHelper.Messages.ServiceUnavailable, _attempts);
				GUI.Label (new Rect(_spacing, _spacing, _width, _lineHeight), message);
				y += _lineHeight;
				y += _spacing;

				if (GUI.Button (new Rect(_spacing, y, _width, _lineHeight), _resourceHelper.Titles.TryAgain))
				{
					_lastAction();
					_lastWebException = null;
					_attempts = 0;
				}
				return;
			}

			//EnsureCultureIsInitialized ();

			if (_viewModel == null)
			{
				Show ();
			}

			GUI.Label (new Rect (_spacing, y, _width, _lineHeight), _resourceHelper.Titles.SetText, _titleStyle);
			y += _lineHeight;
			y += _spacing;

			GUI.Label (new Rect (_spacing, y, _width, _lineHeight), _resourceHelper.Labels.Text);
			y += _lineHeight;

			_viewModel.Text = GUI.TextField (new Rect (_spacing, y, _width, _textBoxHeight), _viewModel.Text ?? "");
			y += _textBoxHeight;
			y += _spacing;

			if (GUI.Button (new Rect (_spacing, y, _width, _lineHeight), _resourceHelper.Titles.SetText)) 
			{
				Save();
			}
			y += _lineHeight;
			y += _spacing;

			if (_viewModel.TextWasSavedMessageVisible) 
			{
				GUI.Label (new Rect (_spacing, y, _width, _lineHeight), _resourceHelper.Messages.Saved);
				y += _lineHeight;
				y += _spacing;
			}

			foreach (var validationMessage in _viewModel.ValidationMessages) 
			{
				GUI.Label (new Rect (_spacing, y, _width, _lineHeight), validationMessage.Text);
				y += _lineHeight;
				y += _spacing;
			}
			
			//if (!String.IsNullOrEmpty(_debugInfo))
			//{
			//	GUI.Label (new Rect(0, 0, 600, 600), "DebugInfo: " + _debugInfo);
			//}
		}
		catch (WebException ex)
		{
			_lastWebException = ex;
			_attempts++;
		}
		catch (Exception ex)
		{
			_lastException = ex;
		}
	}

	private void Show()
	{
		_lastAction = Show;
		using (var appService = CreateServiceClient())
		{
			_viewModel = appService.Show();
		}
	}

	private void Save()
	{
		_lastAction = Save;
		using (var appService = CreateServiceClient())
		{
			_viewModel = appService.Save(_viewModel);
		}
	}

	// Culture

	//private bool _cultureIsInitialized = false;

	//private void EnsureCultureIsInitialized()
	//{
		// TODO: AssigningResourceCulture is not an option now, and AssigningThreadCulture does not work on iOS 6!
		//EnsureCultureIsInitialized_ByAssigningResourceCulture ();
		//EnsureCultureIsInitialized_ByAssigningThreadCulture ();
	//}

	//private void EnsureCultureIsInitialized_ByAssigningResourceCulture()
	//{
		//if (!_cultureIsInitialized)
		//{
			//_cultureIsInitialized = true;
			
			//CultureInfo cultureInfo = GetCultureInfo (_cultureName);
			//ResourceHelper.Labels.Culture = cultureInfo;
			//ResourceHelper.Titles.Culture = cultureInfo;
			//ResourceHelper.Messages.Culture = cultureInfo;
			//PropertyDisplayNames.Culture = cultureInfo;
			//JJ.Framework.Validation.Resources.Messages.Culture = cultureInfo;
		//}
	//}

	/*
	private void EnsureCultureIsInitialized_ByAssigningThreadCulture()
	{
		if (!_cultureIsInitialized)
		{
			_cultureIsInitialized = true;

			CultureInfo cultureInfo = GetCultureInfo (_cultureName);
			Thread.CurrentThread.CurrentUICulture = cultureInfo;
			Thread.CurrentThread.CurrentCulture = cultureInfo;
		}
	}

	private CultureInfo GetCultureInfo(string cultureName)
	{
		// This is compatible with more platforms.
		return new CultureInfo(cultureName);
	}
	*/
	
	// Service Client
	
	private SetTextAppService CreateServiceClient()
	{
		SetTextAppService client = new SetTextAppService ();
		client.Url = _url;
		return client;
	}

	/*
	private SetTextAppServiceClient CreateServiceClient()
	{
		SetTextAppServiceClient client = new SetTextAppServiceClient (
			new BasicHttpBinding(), 
			new EndpointAddress(_url));
		return client;
	}
	*/
}
