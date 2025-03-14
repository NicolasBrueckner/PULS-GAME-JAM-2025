#region

using UnityEngine;
using UnityEngine.InputSystem;

#endregion


//only for movement an abilities
[ RequireComponent( typeof( Rigidbody2D ) ) ]
public class PlayerBehaviour : MonoBehaviour
{
	public float moveSpeed;

	private Rigidbody2D _rb2d;
	private Vector3 _bufferedMovement;

	private PlayerInputEventManager _piem => PlayerInputEventManager.Instance;

	private void Awake()
	{
		_rb2d = GetComponent<Rigidbody2D>();
	}

	private void Start()
	{
		_piem.MovePerformed += OnMovePerformedReceived;
		_piem.MoveCanceled += OnMoveCanceledReceived;
	}

	private void FixedUpdate()
	{
		_rb2d.linearVelocity = _bufferedMovement;
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
		_bufferedMovement = input * moveSpeed;
	}
}