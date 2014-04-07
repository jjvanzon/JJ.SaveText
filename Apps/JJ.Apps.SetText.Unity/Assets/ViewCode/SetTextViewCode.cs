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

public class SetTextViewCode : MonoBehaviour
{
	private SetTextWithSyncViewModel _viewModel;

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

		// TODO: It might not be wise to call it in the game loop.
		ConditionalSynchronize ();
	}

	private void Show()
	{
		using (IContext context = CreateContext()) 
		{
			IEntityRepository entityRepository = new EntityRepository(context);
			var presenter = new SetTextWithSyncPresenter(entityRepository);
			_viewModel = presenter.Show ();
		}
	}

	private void Save()
	{
		using (IContext context = CreateContext()) 
		{
			IEntityRepository entityRepository = new EntityRepository(context);
			var presenter = new SetTextWithSyncPresenter(entityRepository);
			_viewModel = presenter.Save (_viewModel);
		}
	}

	private void ConditionalSynchronize()
	{
		if (_viewModel.TextWasSavedButNotYetSynchronized)
		{
			// TODO: Don't check if internet is available. Check if network is available.
			bool networkIsAvailable = Application.internetReachability != NetworkReachability.NotReachable;
			if (networkIsAvailable)
			{
				Synchronize ();
			}
		}
	}

	private void Synchronize()
	{
		// TODO: Connect to the app service. Call the sync method of the app service. Convert view models from and to SOAP messages.
		//_viewModel = appService.Synchronize(_viewModel);

		// Present synchronization was successful.
		_viewModel.SyncSuccessfulMessageVisible = true;
	}

	private IContext CreateContext()
	{
		//IContext context = new MemoryContext("", typeof(Entity).Assembly, typeof(JJ.Models.SetText.Persistence.Memory.Mapping.EntityMapping).Assembly);
		IContext context = new XmlContext("", typeof(Entity).Assembly, typeof(JJ.Models.SetText.Persistence.Xml.Mapping.EntityMapping).Assembly);
		return context;
	}
}
