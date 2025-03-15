using UnityEngine.UIElements;

public class WinMenuScreen : MenuScreen
{
	private Label _timerLabel;

	public WinMenuScreen( VisualTreeAsset asset, MenuScreenType type, MenuScreenController controller ) : base( asset,
		type, controller )
	{
	}

	private static GameplayEventManager _gem => GameplayEventManager.Instance;

	protected override void GetElements()
	{
		base.GetElements();

		_timerLabel = Root.Q<Label>( "TimerLabel" );
	}

	protected override void BindEvents()
	{
		base.BindEvents();

		_gem.GameEnded += OnGameEnded;
	}

	private void OnGameEnded()
	{
		MenuScreenController.ToggleScreen( Type );
	}
}