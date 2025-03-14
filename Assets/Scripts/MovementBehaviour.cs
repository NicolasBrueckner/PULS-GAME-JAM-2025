#region

using UnityEngine;
using UnityEngine.InputSystem;

#endregion

public class MovementBehaviour : MonoBehaviour
{
	public float moveSpeed;

	private Vector3 _bufferedMovement;

	private PlayerInputEventManager _piem => PlayerInputEventManager.Instance;

	private void Start()
	{
		_piem.MovePerformed += OnMovePerformedReceived;
		_piem.MoveCanceled += OnMoveCanceledReceived;
	}

	private void Update()
	{
		transform.position += _bufferedMovement * moveSpeed * Time.deltaTime;
	}

	private void OnMovePerformedReceived( InputAction.CallbackContext ctx )
	{
		UpdateBufferedMovement( ctx.ReadValue<Vector2>() );
	}

	private void OnMoveCanceledReceived( InputAction.CallbackContext ctx )
	{
		UpdateBufferedMovement( ctx.ReadValue<Vector2>() );
	}

	private void UpdateBufferedMovement( Vector2 input )
	{
		_bufferedMovement = input;
	}
}