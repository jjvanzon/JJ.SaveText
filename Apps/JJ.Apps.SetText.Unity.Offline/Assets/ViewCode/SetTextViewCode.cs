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
using JJ.Apps.SetText.ViewModels;
using JJ.Apps.SetText.Resources;

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

			int y = 50;

			GUI.Label (new Rect (10, y, 200, 20), Titles.SetText);
			y += 30;

			GUI.Label (new Rect (10, y, 200, 20), Labels.Text);
			y += 30;

			_viewModel.Text = GUI.TextField (new Rect (10, y, 200, 160), _viewModel.Text ?? "");
			y += 170;

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
