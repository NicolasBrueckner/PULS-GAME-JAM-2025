#region

using UnityEngine.UIElements;
using Application = UnityEngine.Device.Application;

#endregion

public class GameOverMenuScreen : MenuScreen
{
	private Button _restartButton;
	private Button _mainMenuButton;
	private Button _exitButton;

	public GameOverMenuScreen( VisualTreeAsset asset, MenuScreenType type, MenuScreenController controller ) : base(
		asset, type, controller )
	{
	}

	protected override void GetElements()
	{
		base.GetElements();

		_restartButton = Root.Q<Button>( "RestartButton" );
		_mainMenuButton = Root.Q<Button>( "MainMenuButton" );
		_exitButton = Root.Q<Button>( "ExitButton" );
	}

	protected override void BindElements()
	{
		base.BindElements();

		_restartButton.clicked += OnRestartButtonClicked;
		_mainMenuButton.clicked += OnMainMenuButtonClicked;
		_exitButton.clicked += OnExitButtonClicked;
	}

	private void OnExitButtonClicked()
	{
		Application.Quit();
	}

	private void OnMainMenuButtonClicked()
	{
		GameplayEventManager.Instance.OnGameEnded();
		MenuScreenController.ToggleScreen( MenuScreenType.Main );
	}

	private void OnRestartButtonClicked()
	{
		GameplayEventManager.Instance.OnGameStarted();
		MenuScreenController.ToggleScreen( MenuScreenType.HUD );
	}

	protected override void BindEvents()
	{
		base.BindEvents();

		GameplayEventManager.Instance.PlayerDead += OnPlayerDead;
	}

	private void OnPlayerDead()
	{
		MenuScreenController.ToggleScreen( Type );
	}
}