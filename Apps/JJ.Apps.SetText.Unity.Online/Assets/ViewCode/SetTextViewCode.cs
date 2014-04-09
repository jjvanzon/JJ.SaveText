using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Xml;
using System.ServiceModel;
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
using JJ.Apps.SetText.PresenterInterfaces;
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
		var appService = CreateServiceClient();
		_viewModel = appService.Show();
	}

	private void Save()
	{
		var appService = CreateServiceClient();
		_viewModel = appService.Save(_viewModel);
	}

	private SetTextAppServiceClient CreateServiceClient()
	{
		string url = "http://localhost:51116/SetTextAppService.svc";
		return new SetTextAppServiceClient (url);
	}

	private class SetTextAppServiceClient : ClientBase<ISetTextPresenter>, ISetTextPresenter
	{
		public SetTextAppServiceClient(string url)
			: base(new BasicHttpBinding(), new EndpointAddress(url))
		{ }

		public SetTextViewModel Show()
		{
			SetTextViewModel viewModel = Channel.Show ();
			// TODO: Null-coallesce with a better pattern? Null-coallesce at all?
			viewModel.ValidationMessages = viewModel.ValidationMessages ?? new List<ValidationMessage>();
			return viewModel;
		}

		public SetTextViewModel Save(SetTextViewModel viewModel)
		{
			return Channel.Save (viewModel);
		}
	}
}