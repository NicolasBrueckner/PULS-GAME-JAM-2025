#region

using UnityEngine;
using UnityEngine.InputSystem;

#endregion

public class SceneScrollManager : MonoBehaviour
{
	public GameObject leftCloudControllerGroup;
	public GameObject levelControllerGroup;
	public float minLevelScrollSpeed;
	public float maxLevelScrollSpeed = 20.0f;
	public float leftCloudEnlargeSpeed = 2.0f;
	public float leftCloudRedactSpeed = 1.0f;
	public float dampingFactor = 0.2f;
	public Transform levelExit;
	public Transform player;
	public float screenPadding = 0.5f;

	public GameObject dampLevel0;
	public GameObject dampLevel1;
	public GameObject dampLevel2;
	public GameObject dampLevel3;
	public GameObject mainCamera;

	private float screenWidth;
	private Vector3 initialCloudPosition;
	private Vector3 initialLevelPosition;

	private PlayerInputEventManager _piem => PlayerInputEventManager.Instance;
	private Vector2 movementInput;

	private void Start()
	{
		_piem.MovePerformed += OnMovePerformedReceived;
		_piem.MoveCanceled += OnMoveCanceledReceived;

		screenWidth = Camera.main.orthographicSize * Camera.main.aspect;
		initialCloudPosition = leftCloudControllerGroup.transform.position;
		initialLevelPosition = levelControllerGroup.transform.position;
	}

	private void Update()
	{
		HandleCloudMovement();
		HandleLevelScrolling();
	}

	private void HandleCloudMovement()
	{
		Vector3 cloudPos = leftCloudControllerGroup.transform.position;
		float moveAmount = -movementInput.x * ( movementInput.x > 0 ? leftCloudEnlargeSpeed : leftCloudRedactSpeed ) *
		                   Time.deltaTime;

		cloudPos.x = Mathf.Clamp( cloudPos.x + moveAmount, initialCloudPosition.x,
			initialCloudPosition.x + screenWidth / 2 );
		leftCloudControllerGroup.transform.position = cloudPos;
	}

	private void HandleLevelScrolling()
	{
		if( ClampLevelToExit() ) return; // don't scroll further than exit point
		float playerX = player.transform.position.x;
		float speedFactor = Mathf.InverseLerp( initialLevelPosition.x, screenWidth, playerX );
		float scrollSpeed = Mathf.Lerp( minLevelScrollSpeed, maxLevelScrollSpeed, speedFactor );

		if( !( movementInput.x > 0 ) && !( minLevelScrollSpeed > 0 ) )
			return;

		dampLevel0.transform.position -= new Vector3( scrollSpeed * 1.0f * Time.deltaTime, 0, 0 );
		dampLevel1.transform.position -= new Vector3( scrollSpeed * ( 1.0f - dampingFactor ) * Time.deltaTime, 0, 0 );
		dampLevel2.transform.position -=
			new Vector3( scrollSpeed * ( 1.0f - 2 * dampingFactor ) * Time.deltaTime, 0, 0 );
		dampLevel3.transform.position -=
			new Vector3( scrollSpeed * ( 1.0f - 3 * dampingFactor ) * Time.deltaTime, 0, 0 );
	}

	private bool ClampLevelToExit()
	{
		float exitScreenEdge = mainCamera.transform.position.x + screenWidth - screenPadding;
		return levelExit.position.x < exitScreenEdge;
	}

	private void OnMovePerformedReceived( InputAction.CallbackContext ctx )
	{
		movementInput = ctx.ReadValue<Vector2>();
	}

	private void OnMoveCanceledReceived( InputAction.CallbackContext ctx )
	{
		movementInput = Vector2.zero;
	}
}