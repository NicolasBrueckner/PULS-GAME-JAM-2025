#region

using System;
using UnityEngine;
using UnityEngine.UIElements;

#endregion

public class PauseMenuScreen : MenuScreen
{
	private Button _backButton;
	private Button _mainMenuButton;
	private bool _gamePaused;

	public PauseMenuScreen( VisualTreeAsset asset, MenuScreenType type, MenuScreenController controller ) : base(
		asset, type, controller )
	{
	}

	protected override void OnActivationInternal()
	{
		base.OnActivationInternal();

		TogglePause();
	}

	protected override void OnDeactivationInternal()
	{
		base.OnDeactivationInternal();

		TogglePause();
	}

	protected override void GetElements()
	{
		base.GetElements();

		_backButton = Root.Q<Button>( "BackButton" );
		_mainMenuButton = Root.Q<Button>( "MainMenuButton" );
	}

	protected override void BindElements()
	{
		_backButton.clicked += OnBackButtonClicked;
		_mainMenuButton.clicked += OnMainMenuButtonClicked;
	}

	protected override void BindEvents()
	{
		base.BindEvents();

		PlayerInputEventManager.Instance.PauseGamePerformed += OnPauseGamePerformedReceived;
	}

	private void OnMainMenuButtonClicked()
	{
		GameplayEventManager.Instance.OnGameEnded();
		MenuScreenController.ToggleScreen( MenuScreenType.Main );
	}

	private void OnBackButtonClicked()
	{
		MenuScreenController.ToggleScreen( MenuScreenType.HUD );
	}

	private void OnPauseGamePerformedReceived()
	{
		MenuScreenController.ToggleScreen( _gamePaused ? MenuScreenType.HUD : Type );
	}

	private void TogglePause()
	{
		Time.timeScale = Convert.ToInt32( _gamePaused );
		_gamePaused = !_gamePaused;
	}
}