using SpaceGame.Projectile;
using System;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

namespace SpaceGame.Player
{
    public class PlayerController : MonoBehaviour
    {
        [SerializeField] private PlayerSpaceshipData spaceshipData = default;

        float _zRotation, _lastShootTime;

        public float ThrustInput => _controls.Player.Thrust.ReadValue<float>();
        public float SteeringInput => _controls.Player.Steering.ReadValue<float>();
        public bool IsShootingInput { get; private set; }

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

        private void Start()
        {
            _lastShootTime = Time.time;
        }

        private void OnEnable()
        {
            _controls.Player.Enable();

            _controls.Player.Shoot.performed += ctx => OnShoot(ctx);
            _controls.Player.Shoot.canceled += ctx => OnShoot(ctx);
        }

        private void OnDisable()
        {
            _controls.Player.Disable();

            _controls.Player.Shoot.performed -= ctx => OnShoot(ctx);
            _controls.Player.Shoot.canceled -= ctx => OnShoot(ctx);
        }

        private void FixedUpdate()
        {
            PlayerThrusting(ThrustInput);
            PlayerSteering(SteeringInput);
        }

        private void Update()
        {
            if (IsReadyForShoot()) Shoot();
        }

        #region Player Actions

        #region Shoot
        void OnShoot(InputAction.CallbackContext ctx)
        {
            if (ctx.performed)
                IsShootingInput = true;
            else if (ctx.canceled)
                IsShootingInput = false;
        }
        void Shoot()
        {
            ProjectileCreator.CreateProjectile(
                spaceshipData.projectilePrefab,
                _shotPos.position,
                _shotPos.rotation,
                15,
                5f);

            _lastShootTime = Time.time + spaceshipData.secondsBtwShots;
        }
        bool IsReadyForShoot() => IsShootingInput && _lastShootTime - Time.time < 0;
        #endregion

        #region Movement
        private void PlayerThrusting(float thrustInput)
        {
            if (thrustInput <= 0)
                return;

            var addVel = (Vector2)_transform.up * thrustInput * spaceshipData.ThrustAccelerationSpeed * Time.deltaTime;
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
        #endregion

        void Die()
        {
            SceneManager.LoadScene(0);
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.CompareTag("Enemy")) Die();
        }
    }
}