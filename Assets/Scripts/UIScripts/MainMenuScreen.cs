#region

using UnityEngine;
using UnityEngine.UIElements;

#endregion

public class MainMenuScreen : MenuScreen
{
	private Button _creditsButton;
	private Button _quitButton;
	private Button _startButton;

	public MainMenuScreen( VisualTreeAsset asset, MenuScreenType type, MenuScreenController controller ) : base(
		asset, type, controller )
	{
	}

	protected override void GetElements()
	{
		base.GetElements();

		_startButton = Root.Q<Button>( "StartButton" );
		_creditsButton = Root.Q<Button>( "CreditsButton" );
		_quitButton = Root.Q<Button>( "ExitButton" );
	}

	protected override void BindElements()
	{
		base.BindElements();

		_startButton.clicked += OnStartButtonClicked;
		_creditsButton.clicked += OnCreditsButtonClicked;
		_quitButton.clicked += OnQuitButtonClicked;
	}

	private static void OnStartButtonClicked()
	{
		GameplayEventManager.Instance.OnGameStarted();
	}

	private void OnCreditsButtonClicked()
	{
		MenuScreenController.ToggleScreen( MenuScreenType.Credits );
	}

	private static void OnQuitButtonClicked()
	{
		Application.Quit();
	}
}