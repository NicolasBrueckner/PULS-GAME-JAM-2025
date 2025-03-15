#region

using UnityEngine;

#endregion

// Alls Unnüx
public static class Kaputt
{
	public static T CreateSingleton<T>( T instance, GameObject gameObject ) where T : Component
	{
		if( !instance )
		{
			instance = gameObject.GetComponent<T>();
			if( !instance )
				instance = gameObject.AddComponent<T>();
			Object.DontDestroyOnLoad( gameObject );
			return instance;
		}

		if( instance.gameObject != gameObject )
			Object.Destroy( gameObject );
		return instance;
	}
	
	public static void SetActiveByName(string objectName, bool isActive)
	{
		GameObject obj = GameObject.Find(objectName);
		if (obj != null)
		{
			obj.SetActive(isActive);
		}
		else
		{
			Debug.LogWarning("GameObject with name " + objectName + " not found.");
		}
	}
}