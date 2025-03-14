#region

using System;
using UnityEngine;
using UnityEngine.InputSystem;
using static Kaputt;

#endregion

public class PlayerInputEventManager : MonoBehaviour
{
	public static PlayerInputEventManager Instance;

	public event Action<InputAction.CallbackContext> MovePerformed;
	public event Action<InputAction.CallbackContext> MoveCanceled;


	private PlayerInputs _input;
	private InputAction _moveAction;

	private void Awake()
	{
		Instance = CreateSingleton( Instance, gameObject );
		_input = new();

		SetActions();
		BindActions();
		EnableAllActions();
	}

	private void SetActions()
	{
		_moveAction = _input.Gameplay.Move;
	}

	private void BindActions()
	{
		_moveAction.performed += OnMovePerformed;
		_moveAction.canceled += OnMoveCanceled;
	}

	private void UnbindActions()
	{
		_moveAction.performed -= OnMovePerformed;
		_moveAction.canceled -= OnMoveCanceled;
	}

	private void EnableAllActions()
	{
		_moveAction.Enable();
	}

	private void DisableAllActions()
	{
		_moveAction.Disable();
	}

	#region Event Methods

	private void OnMovePerformed( InputAction.CallbackContext ctx ) => MovePerformed?.Invoke( ctx );
	private void OnMoveCanceled( InputAction.CallbackContext ctx )  => MoveCanceled?.Invoke( ctx );

	#endregion
}