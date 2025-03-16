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
	public event Action ShockwavePerformed;
	public event Action ShockwaveCanceled;
	public event Action BashDashPerformed;
	public event Action BashDashCanceled;
	public event Action PauseGamePerformed;

	private PlayerInputs _input;
	private InputAction _moveAction;
	private InputAction _shockwaveAction;
	private InputAction _bashDashAction;
	private InputAction _pauseAction;

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
		_shockwaveAction = _input.Gameplay.Shockwave;
		_bashDashAction = _input.Gameplay.Bash_Dash;
		_pauseAction = _input.Gameplay.Pause_Game;
	}

	private void BindActions()
	{
		_moveAction.performed += OnMovePerformed;
		_moveAction.canceled += OnMoveCanceled;
		_shockwaveAction.performed += OnShockwavePerformed;
		_shockwaveAction.canceled += OnShockwaveCanceled;
		_bashDashAction.performed += OnBashDashPerformed;
		_bashDashAction.canceled += OnBashDashCanceled;
		_pauseAction.performed += OnPausePerformed;
	}

	private void UnbindActions()
	{
		_moveAction.performed -= OnMovePerformed;
		_moveAction.canceled -= OnMoveCanceled;
		_shockwaveAction.performed -= OnShockwavePerformed;
		_shockwaveAction.canceled -= OnShockwaveCanceled;
		_bashDashAction.performed -= OnBashDashPerformed;
		_bashDashAction.canceled -= OnBashDashCanceled;
		_pauseAction.performed -= OnPausePerformed;
	}

	private void EnableAllActions()
	{
		_moveAction.Enable();
		_shockwaveAction.Enable();
		_bashDashAction.Enable();
		_pauseAction.Enable();
	}

	private void DisableAllActions()
	{
		_moveAction.Disable();
		_shockwaveAction.Disable();
		_bashDashAction.Disable();
		_pauseAction.Disable();
	}

	#region Event Methods

	private void OnMovePerformed( InputAction.CallbackContext ctx )      => MovePerformed?.Invoke( ctx );
	private void OnMoveCanceled( InputAction.CallbackContext ctx )       => MoveCanceled?.Invoke( ctx );
	private void OnShockwavePerformed( InputAction.CallbackContext ctx ) => ShockwavePerformed?.Invoke();
	private void OnShockwaveCanceled( InputAction.CallbackContext ctx )  => ShockwaveCanceled?.Invoke();
	private void OnBashDashPerformed( InputAction.CallbackContext ctx )  => BashDashPerformed?.Invoke();
	private void OnBashDashCanceled( InputAction.CallbackContext ctx )   => BashDashCanceled?.Invoke();
    private void OnPausePerformed(InputAction.CallbackContext ctx) => PauseGamePerformed?.Invoke();

    #endregion
}