using System;
using UnityEngine;

public class VelocityComponent : MonoBehaviour
{
	private Vector2 _previousPosition;
	private float   _step;
	private Vector2 _target;
	private Vector2 _velocity;

	private void Start()
	{
		_previousPosition = transform.position;
	}

	private void FixedUpdate()
	{
		TryMoveToTarget();
		UpdateVelocity();
	}

	public event Action TargetReached;

	private void UpdateVelocity()
	{
		Vector2 deltaPosition = ( Vector2 )transform.position - _previousPosition;

		_velocity         = deltaPosition / Time.fixedDeltaTime;
		_previousPosition = transform.position;
	}

	private void TryMoveToTarget()
	{
		if( Vector2.Distance( transform.position, _target ) > 0.1f )
		{
			MoveToTarget();
			return;
		}

		TargetReached?.Invoke();
	}

	private void MoveToTarget()
	{
		Vector2 previousPosition = transform.position;

		transform.position = Vector2.MoveTowards( transform.position, _target, _step );
		_previousPosition  = previousPosition;
	}

	public void SetMovement( Vector2 target, float speed )
	{
		_target = target;
		_step   = speed * Time.fixedDeltaTime;
	}

	public Vector2 GetVelocity()
	{
		return _velocity;
	}
}