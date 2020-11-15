using System.Collections;
using UnityEngine;

namespace SpaceGame.EnemyNamespace
{
    public class Asteroid : Enemy
    {
        [Header("General")]
        [SerializeField] private int maxHealth = default;

        [Header("Movement")]
        [SerializeField] private float flySpeed = default;
        [SerializeField] private float fullAccelerationTime = default;

        Vector2 _dir;
        bool _isInit;

        public float FlySpeed
        {
            get { return _speed; }
            set { _speed = Mathf.Clamp(value, 0, flySpeed); }
        }
        float _speed;

        public int Health
        {
            get { return _health; }
            set { _health = Mathf.Clamp(value, 0, maxHealth); }
        }
        int _health;

        Transform _transform;
        Collider2D _collider;

        private void Awake()
        {
            _transform = transform;
            _collider = GetComponent<Collider2D>();
            _collider.enabled = false;
        }

        private void Update()
        {
            if (!_isInit) return;

            _transform.Translate(_dir * FlySpeed * Time.deltaTime);
        }

        #region Enemy
        public override void Init()
        {
            _isInit = true;
            Health = maxHealth;
            _dir = GetRandomDirection();
            StartCoroutine(AccelerationCoroutine());
            _collider.enabled = true;
        }

        public override void TakeDamage(int dmg)
        {
            Health -= dmg;

            if (Health <= 0) Die();
        }

        public override void Die() => gameObject.SetActive(false);

        private void OnDisable()
        {
            StopAllCoroutines();
            _collider.enabled = false;
            FlySpeed = 0f;
        }
        #endregion

        #region Movement
        IEnumerator AccelerationCoroutine()
        {
            float step = flySpeed * Time.deltaTime / fullAccelerationTime;
            while (FlySpeed < flySpeed)
            {
                FlySpeed += step;
                yield return null;
            }
        }

        Vector3 GetRandomDirection()
        {
            float deg = Random.Range(0f, 2 * Mathf.PI) * Mathf.Rad2Deg;
            return new Vector2(Mathf.Cos(deg), Mathf.Sin(deg)).normalized;
        }
        #endregion
    }
}