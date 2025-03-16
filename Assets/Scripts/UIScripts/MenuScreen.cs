#region

using System;
using UnityEngine.UIElements;

#endregion

public enum MenuScreenType
{
	Main,
	HUD,
	Credits,
	Win,
	Pause,
	GameOver,
}

[ Serializable ]
public class MenuScreen
{
	public VisualTreeAsset screenAsset;

	protected MenuScreenController MenuScreenController;

	public MenuScreen( VisualTreeAsset asset, MenuScreenType type, MenuScreenController controller )
	{
		SetDefaults( asset, type, controller );
	}

	public VisualElement Root{ get; private set; }
	public MenuScreenType Type{ get; private set; }

	protected virtual void SetDefaults( VisualTreeAsset asset, MenuScreenType type,
		MenuScreenController controller )
	{
		screenAsset = asset;
		Type = type;
		MenuScreenController = controller;

		Root = screenAsset.CloneTree();

		GetElements();
		BindElements();
		BindEvents();
	}

	public void OnActivation()
	{
		OnActivationInternal();
	}

	public void OnDeactivation()
	{
		OnDeactivationInternal();
	}

	protected virtual void OnActivationInternal()
	{
	}

	protected virtual void OnDeactivationInternal()
	{
	}

	protected virtual void GetElements()
	{
	}

	protected virtual void BindElements()
	{
	}

	protected virtual void BindEvents()
	{
	}
}