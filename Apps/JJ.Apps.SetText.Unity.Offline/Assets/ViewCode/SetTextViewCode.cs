// Offline

using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Xml;
using System.Globalization;
using System.Threading;
using System.Reflection;
using JJ.Framework.Common;
using JJ.Framework.Logging;
using JJ.Framework.Persistence;
using JJ.Framework.Persistence.Memory;
using JJ.Framework.Persistence.Xml.Linq;
using JJ.Business.SetText;
using JJ.Business.SetText.Resources;
using JJ.Models.SetText;
using JJ.Models.SetText.Persistence.Repositories;
using JJ.Models.SetText.Persistence.RepositoryInterfaces;
using JJ.Models.SetText.Persistence.Memory.Mapping;
using JJ.Models.SetText.Persistence.Xml.Linq.Mapping;
using JJ.Apps.SetText.Presenters;
using JJ.Apps.SetText.Interface.ViewModels;
using JJ.Apps.SetText.Resources;
using JJ.Framework.PlatformCompatibility;

public class SetTextViewCode : MonoBehaviour
{
	private SetTextViewModel _viewModel;

	private string _cultureName = "nl-NL";

	private Exception _lastException;
	private string _debugInfo;

	private int _width = 200;
	private int _lineHeight = 24;
	private int _spacing = 10;
	private int _textBoxHeight = 160;
	private GUIStyle _titleStyle;

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

			int y = _spacing;

			GUI.Label (new Rect (_spacing, y, _width, _lineHeight), Titles.SetText, _titleStyle);
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
		catch (Exception ex)
		{
			_lastException = ex;
		}
	}

	private void Show()
	{
		using (IContext context = CreateContext()) 
		{
			IEntityRepository entityRepository = new EntityRepository(context);
			var presenter = new SetTextPresenter(entityRepository);
			_viewModel = presenter.Show ();
		}
	}

	private void Save()
	{
		using (IContext context = CreateContext()) 
		{
			IEntityRepository entityRepository = new EntityRepository(context);
			var presenter = new SetTextPresenter(entityRepository);
			_viewModel = presenter.Save (_viewModel);
		}
	}

	// Culture

	private void InitializeCulture()
	{
		// Assigning thread culture is not possible on iOS 6.
		// So assign resource cultures instead.

		CultureInfo cultureInfo = CultureInfo_PlatformSafe.GetCultureInfo(_cultureName);
		Labels.Culture = cultureInfo;
		Titles.Culture = cultureInfo;
		Messages.Culture = cultureInfo;
		PropertyDisplayNames.Culture = cultureInfo;
		JJ.Framework.Validation.Resources.Messages.Culture = cultureInfo;
	}

	// Persistence

	private IContext CreateContext()
	{
		return CreateXmlContext ();
	}

	private IContext CreateMemoryContext()
	{
		Assembly modelAssembly = typeof(Entity).Assembly;
		Assembly mappingAssembly = typeof(JJ.Models.SetText.Persistence.Memory.Mapping.EntityMapping).Assembly;
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
		Assembly mappingAssembly = typeof(JJ.Models.SetText.Persistence.Xml.Linq.Mapping.EntityMapping).Assembly;
		return new XmlContext(folderPath, modelAssembly, mappingAssembly);
	}
}
