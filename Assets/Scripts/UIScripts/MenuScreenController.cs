#region

using System.Linq;
using AYellowpaper.SerializedCollections;
using UnityEngine;
using UnityEngine.UIElements;
using static Kaputt;

#endregion

public class MenuScreenController : MonoBehaviour
{
	private static MenuScreenController Instance;

	public MenuScreenType startingScreen;
	public UIDocument rootDocument;
	public SerializedDictionary<MenuScreenType, MenuScreen> screensByType = new();

	private MenuScreen _currentScreen;
	private VisualElement _root;

	private void Awake()
	{
		Instance = CreateSingleton( Instance, gameObject );
	}

	private void Start()
	{
		_root = rootDocument.rootVisualElement;

		InitializeScreens();
		AddScreensToRoot();
		ToggleScreen( startingScreen );
	}

	private void InitializeScreens()
	{
		foreach( MenuScreenType type in screensByType.Keys.ToList() )
		{
			VisualTreeAsset asset = screensByType[ type ].screenAsset;
			MenuScreen menuScreen = MenuScreenFactory.CreateScreen( asset, type, this );

			screensByType[ type ] = menuScreen;
		}
	}

	private void AddScreensToRoot()
	{
		foreach( VisualElement current in from t in screensByType.Keys.ToList() select screensByType[ t ].Root )
		{
			current.style.display = DisplayStyle.None;
			current.AddToClassList( "menu-screen" );
			_root.Add( current );
		}
	}

	public void ToggleScreen( MenuScreenType screenType )
	{
		if( !screensByType.ContainsKey( screenType ) )
			return;

		if( _currentScreen != null )
		{
			_currentScreen.Root.style.display = DisplayStyle.None;
			_currentScreen.OnDeactivation();
		}

		_currentScreen = screensByType[ screenType ];
		_currentScreen.OnActivation();
		_currentScreen.Root.style.display = DisplayStyle.Flex;
	}

	public MenuScreen GetScreenByType( MenuScreenType screenType ) => screensByType[ screenType ];
}