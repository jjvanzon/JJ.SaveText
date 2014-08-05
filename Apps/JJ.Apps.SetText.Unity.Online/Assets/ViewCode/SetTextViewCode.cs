// Online

using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Xml;
using System.Globalization;
using System.Threading;
using System.Net;
using JJ.Framework.Logging;
using JJ.Apps.SetText.ViewModels;
using JJ.Apps.SetText.AppService.Interface;
using JJ.Apps.SetText.AppService.Interface.CustomClient;
using JJ.Apps.SetText.Unity.Online;
using JJ.Apps.SetText.PresenterInterfaces;

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
		ISetTextPresenter appService = CreateServiceClient ();
		_viewModel = appService.Show();
	}

	private void Save()
	{
		_lastAction = Save;
		ISetTextPresenter appService = CreateServiceClient ();
		_viewModel = appService.Save(_viewModel);
	}

	private ISetTextPresenter CreateServiceClient()
	{
		var client = new SetTextAppServiceClient (_url, _cultureName);
		return client;
	}
}
