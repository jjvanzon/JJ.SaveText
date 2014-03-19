﻿using UnityEngine;
using System.Collections;
using JJ.Framework.Persistence;
using JJ.Framework.Persistence.Xml;
using JJ.Models.SetText;
using JJ.Models.SetText.Persistence.Xml.Mapping;
using JJ.Apps.SetText.Presenters;
using JJ.Models.SetText.Persistence.Repositories;
using JJ.Models.SetText.Persistence.RepositoryInterfaces;
using JJ.Apps.SetText.ViewModels;
using JJ.Apps.SetText.Resources;

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
		if (_viewModel == null)
		{
			Show ();
		}

		GUI.Label (new Rect (10, 50, 200, 20), Titles.SetText);

		GUI.Label (new Rect (10, 80, 200, 20), Labels.Text);

		_viewModel.Text = GUI.TextField (new Rect (10, 110, 200, 200), _viewModel.Text ?? "");

		if (GUI.Button (new Rect (10, 320, 200, 20), Titles.SetText)) 
		{
			//Save();
		}
	}

	private void Show()
	{
		_viewModel = new SetTextViewModel ();

		/*using (IContext context = CreateContext()) 
		{
			IEntityRepository entityRepository = new EntityRepository(context);
			SetTextPresenter presenter = new SetTextPresenter(entityRepository);
			_viewModel = presenter.Show ();
		}*/
	}

	private void Save()
	{
		/*using (IContext context = CreateContext()) 
		{
			IEntityRepository entityRepository = new EntityRepository(context);
			SetTextPresenter presenter = new SetTextPresenter(entityRepository);
			_viewModel = presenter.Save (_viewModel);
		}*/
	}

	private IContext CreateContext()
	{
		IContext context = new XmlContext (
			folderPath: "",
			modelAssembly: typeof(Entity).Assembly,
			mappingAssembly: typeof(EntityMapping).Assembly);

		return context;
	}
}
