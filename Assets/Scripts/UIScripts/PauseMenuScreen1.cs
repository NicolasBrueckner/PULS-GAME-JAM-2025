using System;
using UnityEngine;
using UnityEngine.UIElements;

public class PauseMenuScreen : MenuScreen
{
	private Button _backButton;
    private bool gamePaused = false;

    public PauseMenuScreen( VisualTreeAsset asset, MenuScreenType type, MenuScreenController controller ) : base(
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

    protected override void BindEvents()
    {
        base.BindEvents();
		PlayerInputEventManager.Instance.PauseGamePerformed += OnBackButtonClicked;
    }

    private void OnBackButtonClicked()
	{
		Debug.Log("Menu pause");
        Time.timeScale = Convert.ToInt32(gamePaused);
        gamePaused = !gamePaused;
		if (gamePaused)
			MenuScreenController.ToggleScreen(Type);
		else
			MenuScreenController.ToggleScreen(MenuScreenType.HUD);
    }
}