#region

using UnityEngine;
using UnityEngine.InputSystem;

#endregion


public class MovementBehaviour : MonoBehaviour
{
	public float moveSpeed;
	private Vector3 _bufferedMovement;
	private float _currentMoveSpeed;

	private Rigidbody _rb;

	private PlayerInputEventManager _piem => PlayerInputEventManager.Instance;
	private GameplayEventManager _gem => GameplayEventManager.Instance;

	private void Awake()
	{
		_rb = GetComponent<Rigidbody>();
		_currentMoveSpeed = moveSpeed;
	}

	private void Start()
	{
		_piem.MovePerformed += OnMovePerformedReceived;
		_piem.MoveCanceled += OnMoveCanceledReceived;
		_gem.MoveSpeedChange += UpdateCurrentMoveSpeed;
	}

	private void FixedUpdate()
	{
		_rb.linearVelocity = _bufferedMovement;
	}

	private void OnMovePerformedReceived( InputAction.CallbackContext ctx )
	{
		UpdateBufferedMovement( ctx.ReadValue<Vector2>() );
	}

	private void OnMoveCanceledReceived( InputAction.CallbackContext ctx )
	{
		UpdateBufferedMovement( ctx.ReadValue<Vector2>() );
	}

	private void UpdateCurrentMoveSpeed( float factor )
	{
		_currentMoveSpeed = moveSpeed * factor;
	}

	private void UpdateBufferedMovement( Vector2 input )
	{
		_bufferedMovement = input * _currentMoveSpeed;
	}
}