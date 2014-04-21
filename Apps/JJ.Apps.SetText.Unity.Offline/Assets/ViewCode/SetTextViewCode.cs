using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Xml;
using JJ.Framework.Persistence;
using JJ.Framework.Persistence.Memory;
using JJ.Framework.Persistence.Xml;
using JJ.Models.SetText;
using JJ.Models.SetText.Persistence.Memory.Mapping;
using JJ.Models.SetText.Persistence.Xml.Mapping;
using JJ.Models.SetText.Persistence.Repositories;
using JJ.Models.SetText.Persistence.RepositoryInterfaces;
using JJ.Business.SetText;
using JJ.Apps.SetText.Presenters;
using JJ.Apps.SetText.ViewModels;
using JJ.Apps.SetText.Resources;
using System.Globalization;
using System.Threading;

public class SetTextViewCode : MonoBehaviour
{
	private SetTextViewModel _viewModel;

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
		// Don't know how to do it properly.
		if (CultureInfo.CurrentUICulture.Name == "en-US" ||
		    CultureInfo.CurrentUICulture.Name == "") 
		{
			Thread.CurrentThread.CurrentUICulture = CultureInfo.GetCultureInfo("nl-NL");
			Thread.CurrentThread.CurrentCulture = CultureInfo.GetCultureInfo("nl-NL");
		}

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

		//string folderPath = "";
		string folderPath = Application.persistentDataPath;
		return new XmlContext(folderPath, typeof(Entity).Assembly, typeof(JJ.Models.SetText.Persistence.Xml.Mapping.EntityMapping).Assembly);
	}
}
