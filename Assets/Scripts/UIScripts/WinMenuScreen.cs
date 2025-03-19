#region

using UnityEngine.UIElements;

#endregion

public class WinMenuScreen : MenuScreen
{
	private Button _mainMenuButton;

	public WinMenuScreen( VisualTreeAsset asset, MenuScreenType type, MenuScreenController controller ) : base( asset,
		type, controller )
	{
	}

	protected override void GetElements()
	{
		base.GetElements();

		_mainMenuButton = Root.Q<Button>( "MainMenuButton" );
	}

	protected override void BindElements()
	{
		base.BindElements();

		_mainMenuButton.clicked += OnMainMenuButtonClicked;
	}

	private void OnMainMenuButtonClicked()
	{
		GameplayEventManager.Instance.OnGameEnded();
		MenuScreenController.ToggleScreen( MenuScreenType.Main );
	}

	protected override void BindEvents()
	{
		base.BindEvents();

		GameplayEventManager.Instance.GameWon += OnGameWon;
	}

	private void OnGameWon()
	{
		MenuScreenController.ToggleScreen( Type );
	}
}