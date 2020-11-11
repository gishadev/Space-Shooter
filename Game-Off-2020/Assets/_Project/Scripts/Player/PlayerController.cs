using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private float thrustSpeed;
    [SerializeField] private float steeringSpeed;

    PlayerInput _controls;

    private void Awake()
    {
        _controls = new PlayerInput();
    }

    private void OnEnable()
    {
        _controls.Player.Shoot.performed += ShootPerformed;
        _controls.Player.Movement.performed += OnMovement;

        _controls.Player.Enable();
    }

    private void OnDisable()
    {
        _controls.Player.Shoot.performed -= ShootPerformed;
        _controls.Player.Movement.performed -= OnMovement;

        _controls.Player.Disable();
    }


    #region Player Actions
    private void ShootPerformed(InputAction.CallbackContext ctx)
    {
        Debug.Log("Shoot Performed!");
    }

    private void OnMovement(InputAction.CallbackContext ctx)
    {
        Debug.LogFormat("Movement is {0}", ctx.ReadValue<Vector2>());
    }
    #endregion
}
