#region

using System.Collections.Generic;
using UnityEngine;

#endregion

public class FixedUpdateEventManager : MonoBehaviour
{
	private static readonly List<IFixedUpdateObserver> _observers = new();
	private static readonly List<IFixedUpdateObserver> _pendingObservers = new();
	private static int _currentIndex;

	private void FixedUpdate()
	{
		for( _currentIndex = _observers.Count - 1; _currentIndex >= 0; _currentIndex-- )
			_observers[ _currentIndex ].ObservedFixedUpdate();

		_observers.AddRange( _pendingObservers );
		_pendingObservers.Clear();
	}

	public static void RegisterObserver( IFixedUpdateObserver observer )
	{
		_pendingObservers.Add( observer );
	}

	public static void UnregisterObserver( IFixedUpdateObserver observer )
	{
		_pendingObservers.Remove( observer );
		_currentIndex--;
	}
}