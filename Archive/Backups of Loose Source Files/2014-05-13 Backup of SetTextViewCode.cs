using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Xml;
using JJ.Framework.Common;
using JJ.Framework.Persistence;
//using JJ.Framework.Persistence.Memory;
//using JJ.Framework.Persistence.Xml;
using JJ.Framework.Persistence.Xml.Linq;
using JJ.Models.SetText;
//using JJ.Models.SetText.Persistence.Memory.Mapping;
//using JJ.Models.SetText.Persistence.Xml.Mapping;
using JJ.Models.SetText.Persistence.Xml.Linq.Mapping;
using JJ.Models.SetText.Persistence.Repositories;
using JJ.Models.SetText.Persistence.RepositoryInterfaces;
using JJ.Business.SetText;
using JJ.Apps.SetText.Presenters;
using JJ.Apps.SetText.ViewModels;
using JJ.Apps.SetText.Resources;
using System.Globalization;
using System.Threading;
using System;

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
				/*if (_lastException.InnerException != null)
				{
					_lastException = _lastException.InnerException;
				}*/
				string exceptionMessage = ExceptionHelper.FormatExceptionWithInnerExceptions(_lastException, includeStackTrace: true);
				GUI.Label (new Rect(0, 0, 580, 1500), exceptionMessage);
				//GUI.Label (new Rect(0, 25, 600, 1200), _lastException.StackTrace);
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

	/*private void EnsureCultureIsInitialized()
	{
		if (CultureInfo.CurrentUICulture.Name == "en-US" ||
			CultureInfo.CurrentUICulture.Name == "") 
		{
			CultureInfo cultureInfo = GetCultureInfo ("nl-NL");
			JJ.Apps.SetText.Resources.Labels.Culture = cultureInfo;
			JJ.Apps.SetText.Resources.Titles.Culture = cultureInfo;
			JJ.Apps.SetText.Resources.Messages.Culture = cultureInfo;
			JJ.Business.SetText.Resources.PropertyDisplayNames.Culture = cultureInfo;
			// Oops, not public.
			//JJ.Framework.Validation.Resources
		}
	}*/

	private void EnsureCultureIsInitialized()
	{
		// 2014-05-11: Temporarily skip this to get it to run in iOS.
		//return;

		if (_debugInfo == null)
		{
			_debugInfo = "";
			_debugInfo += "EnsureCultureIsInitialized()" + Environment.NewLine;
			_debugInfo += "Initial CurrentUICulture = " + CultureInfo.CurrentUICulture.Name + ", ";
			_debugInfo += "Initial CurrentCulture = " + CultureInfo.CurrentCulture.Name + Environment.NewLine;
		}

		// Don't know how to do it properly in Unity.
		if (CultureInfo.CurrentUICulture.Name == "en-US" ||
		    CultureInfo.CurrentUICulture.Name == "")
		{
			CultureInfo cultureInfo = GetCultureInfo("nl-NL");
			Thread.CurrentThread.CurrentUICulture = cultureInfo;
			_debugInfo += "CurrentUICulture changed to nl-NL, ";
		}

		if (CultureInfo.CurrentCulture.Name == "en-US" ||
		    CultureInfo.CurrentCulture.Name == "") 
		{
			CultureInfo cultureInfo = GetCultureInfo("nl-NL");
			Thread.CurrentThread.CurrentCulture = cultureInfo;
			_debugInfo += "CurrentCulture changed to nl-NL, ";
		}

		
		if (Messages.Culture == null ||
			Messages.Culture.Name == "en-US" ||
		    Messages.Culture.Name == "") 
		{
			CultureInfo cultureInfo = GetCultureInfo("nl-NL");
			Messages.Culture = cultureInfo;
			_debugInfo += "Messages.Culture changed to nl-NL, ";
		}
		
		if (Labels.Culture == null ||
		    Labels.Culture.Name == "en-US" ||
		    Labels.Culture.Name == "") 
		{
			CultureInfo cultureInfo = GetCultureInfo("nl-NL");
			Labels.Culture = cultureInfo;
			_debugInfo += "Labels.Culture changed to nl-NL, ";
		}
		
		if (Titles.Culture == null ||
		    Titles.Culture.Name == "en-US" ||
		    Titles.Culture.Name == "") 
		{
			CultureInfo cultureInfo = GetCultureInfo("nl-NL");
			Titles.Culture = cultureInfo;
			_debugInfo += "Titles.Culture changed to nl-NL, ";
		}
	}

	private CultureInfo GetCultureInfo(string cultureName)
	{
		// On iOS the CultureInfo constructor does not work, because its implementation needs JIT compilation, which is not supported.
		// But I think I remember that the GetCultureInfo has compatibilities with another platform (Android?)
		//if (Application.platform == RuntimePlatform.IPhonePlayer) 
		//{
			return CultureInfo.GetCultureInfo (cultureName);
		//}
		//else
		//{
		//	return new CultureInfo(cultureName);
		//}
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

	private IContext CreateContext()
	{
		//return new MemoryContext("", typeof(Entity).Assembly, typeof(JJ.Models.SetText.Persistence.Memory.Mapping.EntityMapping).Assembly);

		// Windows Phone will not take the absolute location that Application.persistentDataPath.
		// TODO: Is there a better property to use than Application.persistentDataPath, that will work on all the platforms we target?
		string folderPath;
		if (Application.platform == RuntimePlatform.WP8Player) 
		{
			folderPath = "";
		}
		else
		{
			 folderPath = Application.persistentDataPath;
		}

		//return new XmlContext(folderPath, typeof(Entity).Assembly, typeof(JJ.Models.SetText.Persistence.Xml.Mapping.EntityMapping).Assembly);
		return new XmlContext(folderPath, typeof(Entity).Assembly, typeof(JJ.Models.SetText.Persistence.Xml.Linq.Mapping.EntityMapping).Assembly);
	}
}
