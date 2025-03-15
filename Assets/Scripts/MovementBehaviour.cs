#region

using UnityEngine;
using UnityEngine.InputSystem;

#endregion


//only for movement an abilities
[ RequireComponent( typeof( Rigidbody2D ) ) ]
public class MovementBehaviour : MonoBehaviour
{
	public float moveSpeed;
    public GameObject movementBounds;

    private Rigidbody2D _rb2d;
	private Vector3 _bufferedMovement;
	private float _currentMoveSpeed;

	private PlayerInputEventManager _piem => PlayerInputEventManager.Instance;
	private GameplayEventManager _gem => GameplayEventManager.Instance;

	private void Awake()
	{
		_rb2d = GetComponent<Rigidbody2D>();
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
        if (movementBounds != null)
        {
            Vector3 newPosition = _rb2d.position + (Vector2)_bufferedMovement * Time.fixedDeltaTime;
            if (IsWithinBounds(newPosition))
            {
                _rb2d.linearVelocity = _bufferedMovement;
            }
            else
            {
                _rb2d.linearVelocity = Vector2.zero;
            }
        }
        else
        {
            _rb2d.linearVelocity = _bufferedMovement;
        }
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

    private bool IsWithinBounds(Vector3 targetPosition)
    {
        if (movementBounds == null) return true;

        Bounds bounds = movementBounds.GetComponent<Collider>().bounds;
        return bounds.Contains(targetPosition);
    }
}