using SpaceGame.Core;
using System.Collections;
using UnityEngine;

namespace SpaceGame.EnemyNamespace
{
    public class Asteroid : Enemy
    {
        [Header("Asteroid")]
        [SerializeField] private float flySpeed = default;
        [SerializeField] private float fullAccelerationTime = default;

        Vector2 _dir;

        public float FlySpeed
        {
            get { return _speed; }
            set { _speed = Mathf.Clamp(value, 0, flySpeed); }
        }
        float _speed;

        Transform _transform;

        private void Awake()
        {
            _transform = transform;
        }

        private void Update()
        {
            _transform.Translate(_dir * FlySpeed * Time.deltaTime);
        }

        private void OnDisable()
        {
            StopAllCoroutines();
            FlySpeed = 0f;
        }

        #region Enemy
        public override void OnSpawn()
        {
            base.OnSpawn();
            RestoreHealth();
            _dir = GetRandomDirection();
            StartCoroutine(AccelerationCoroutine());
        }

        public override void TakeDamage(int dmg)
        {
            Health -= dmg;

            if (Health <= 0) Die();
        }

        public override void Die()
        {
            ScoreManager.AddScore(25);
            gameObject.SetActive(false);
        }
        #endregion

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
    }
}