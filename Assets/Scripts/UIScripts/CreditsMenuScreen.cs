using UnityEngine.UIElements;

public class CreditsMenuScreen : MenuScreen
{
	private Button _backButton;

	public CreditsMenuScreen( VisualTreeAsset asset, MenuScreenType type, MenuScreenController controller ) : base(
		asset, type, controller )
	{
	}

	protected override void GetElements()
	{
		base.GetElements();

		_backButton = Root.Q<Button>( "BackButton" );
	}

	protected override void BindElements()
	{
		_backButton.clicked += OnBackButtonClicked;
	}

	private void OnBackButtonClicked()
	{
		MenuScreenController.ToggleScreen( MenuScreenType.Main );
	}
}