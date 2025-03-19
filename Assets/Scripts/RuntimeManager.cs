#region

using UnityEngine;
using UnityEngine.SceneManagement;
using static Kaputt;

#endregion

public class RuntimeManager : MonoBehaviour
{
	private static RuntimeManager Instance{ get; set; }

	public int initSceneIndex;
	public int mainSceneIndex;

	private static GameplayEventManager _gem => GameplayEventManager.Instance;

	private void Awake()
	{
		Instance = CreateSingleton( Instance, gameObject );
		DontDestroyOnLoad( gameObject );

		_gem.GameStarted += OnGameStarted;
		_gem.GameEnded += OnGameEnded;
	}

	private void OnGameEnded()
	{
		SceneManager.LoadScene( initSceneIndex, LoadSceneMode.Single );
	}

	private void OnGameStarted()
	{
		SceneManager.LoadScene( mainSceneIndex, LoadSceneMode.Single );
	}
}