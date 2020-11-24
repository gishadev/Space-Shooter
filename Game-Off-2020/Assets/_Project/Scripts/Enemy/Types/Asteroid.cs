using SpaceGame.Core;
using System.Collections;
using UnityEngine;

namespace SpaceGame.EnemyLogic
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

        public override void Die()
        {
            base.Die();
            ScoreManager.AddScore(25);
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

        Vector2 GetRandomDirection()
        {
            float deg = Random.Range(0f, 2 * Mathf.PI) * Mathf.Rad2Deg;
            return new Vector2(Mathf.Cos(deg), Mathf.Sin(deg)).normalized;
        }

        Vector2 GetReflectDirection(Collider2D collider)
        {
            Vector2 dir = (collider.transform.position - _transform.position).normalized;
            LayerMask enemyLayer = LayerMask.NameToLayer("Enemy");
            RaycastHit2D hitInfo = Physics2D.Raycast(_transform.position, dir, 0.25f, 1 << enemyLayer);
            
            return Vector2.Reflect(dir, hitInfo.normal);
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.CompareTag("Enemy")) Reflect(other);
        }

        private void Reflect(Collider2D other)
        {
            _dir = GetReflectDirection(other);
            Effects.Audio.AudioManager.Instance.PlaySFX("Asteroid_Collision");
        }
    }
}