#region

using UnityEngine.UIElements;

#endregion

public class HUDMenuScreen : MenuScreen
{
	private Label _timerLabel;

	public HUDMenuScreen( VisualTreeAsset asset, MenuScreenType type, MenuScreenController controller ) : base(
		asset, type, controller )
	{
	}

	private static GameplayEventManager _gem => GameplayEventManager.Instance;


	protected override void GetElements()
	{
		base.GetElements();
	}
}