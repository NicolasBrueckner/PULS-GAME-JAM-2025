using UnityEngine;

[ RequireComponent( typeof( VelocityComponent ) ) ]
public class FollowPathComponent : MonoBehaviour
{
	public Vector3[] points;
	public float     speed   = 1;
	public bool      forward = true;
	public bool      loop;

	private int               _currentIndex;
	private VelocityComponent _moveComponent;

	private void Awake()
	{
		_moveComponent               =  GetComponent<VelocityComponent>();
		_moveComponent.TargetReached += UpdateCurrentIndex;

		if( points.Length > 0 )
			transform.position = points[ 0 ];

		_currentIndex = 1;
	}

	private void FixedUpdate()
	{
		if( points.Length == 0 )
			return;

		_moveComponent.SetMovement( points[ _currentIndex ], speed );
	}

	//updates the next target to move to
	//back and forth or loops through them
	private void UpdateCurrentIndex()
	{
		int last = points.Length - 1;
		_currentIndex += forward ? 1 : -1;

		if( _currentIndex <= last && _currentIndex >= 0 )
			return;

		if( loop )
		{
			_currentIndex = forward ? 0 : last;
		}
		else
		{
			_currentIndex = Mathf.Clamp( _currentIndex, 0, last );
			forward       = !forward;
		}
	}
}