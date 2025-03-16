using UnityEngine.UIElements;

public class WinMenuScreen : MenuScreen
{
	private Button _mainMenuButton;
	
	public WinMenuScreen( VisualTreeAsset asset, MenuScreenType type, MenuScreenController controller ) : base( asset,
		type, controller )
	{
	}

	private static GameplayEventManager _gem => GameplayEventManager.Instance;

	protected override void GetElements()
	{
		base.GetElements();
		
		_mainMenuButton=Root.Q<Button>( "MainMenuButton" );
	}

	protected override void BindElements()
	{
		base.BindElements();

		_mainMenuButton.clicked += OnMainMenuButtonClicked;
	}

	private void OnMainMenuButtonClicked()
	{
		_gem.OnGameEnded();
	}

	protected override void BindEvents()
	{
		base.BindEvents();

		_gem.GameWon += OnGameWon;
	}

	private void OnGameWon()
	{
		MenuScreenController.ToggleScreen( Type );
	}
}