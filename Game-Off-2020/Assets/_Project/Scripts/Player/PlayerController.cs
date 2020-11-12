using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private PlayerSpaceshipData spaceshipData;

    float _zRotation;

    public float ThrustInput { get => _controls.Player.Thrust.ReadValue<float>(); }
    public float SteeringInput { get => _controls.Player.Steering.ReadValue<float>(); }

    PlayerInput _controls;
    Rigidbody2D _rb;
    Transform _transform;
    Transform _shotPos;

    private void Awake()
    {
        _controls = new PlayerInput();
        _rb = GetComponent<Rigidbody2D>();
        _transform = transform;

        _shotPos = Array.Find(transform.GetComponentsInChildren<Transform>(), x => x.name == "ShotPos");
        if (_shotPos == null)
            Debug.LogError("'ShotPos' wasn't found!");
    }

    private void OnEnable()
    {
        _controls.Player.Shoot.performed += ShootPerformed;

        _controls.Player.Enable();
    }

    private void OnDisable()
    {
        _controls.Player.Shoot.performed -= ShootPerformed;

        _controls.Player.Disable();
    }

    private void FixedUpdate()
    {
        PlayerThrusting(ThrustInput);
        PlayerSteering(SteeringInput);
    }

    #region Player Actions
    private void ShootPerformed(InputAction.CallbackContext ctx)
    {
        ProjectileInstantiater instantiater = new BurstProjectileInstantiater(
            spaceshipData.projectilePrefab,
            _shotPos.position,
            _transform.rotation, 15, 5f);

        ProjectileCreator.CreateProjectile(instantiater);
    }

    private void PlayerThrusting(float thrustInput)
    {
        if (thrustInput <= 0)
            return;

        Vector2 addVel = (Vector2)_transform.up * thrustInput * spaceshipData.ThrustAccelerationSpeed * Time.deltaTime;
        _rb.velocity = Vector2.ClampMagnitude(_rb.velocity + addVel, spaceshipData.ThrustMaxSpeed);
    }

    private void PlayerSteering(float steeringInput)
    {
        if (steeringInput == 0)
            return;

        _zRotation -= steeringInput * spaceshipData.SteeringSpeed * Time.deltaTime;
        _transform.rotation = Quaternion.Euler(Vector3.forward * _zRotation);
    }
    #endregion
}
