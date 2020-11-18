using SpaceGame.Core;
using System.Collections;
using UnityEngine;

namespace SpaceGame.EnemyLogic
{
    public class Turret : Enemy
    {
        [Header("Turret")]
        [SerializeField] private GameObject projPrefab = default;
        [SerializeField] private float shootDelay = default;

        Transform _transform, _target;

        private void Awake()
        {
            _transform = transform;
        }

        private void Start()
        {
            _target = GameObject.FindGameObjectWithTag("Player").transform;

            if (_target == null)
            {
                Debug.LogError("Target is not available!");
                return;
            }
        }

        private void Update()
        {
            AimOnTarget();
        }

        private void OnDisable()
        {
            StopAllCoroutines();
        }

        #region Enemy
        public override void OnSpawn()
        {
            base.OnSpawn();
            RestoreHealth();
            StartCoroutine(ShootLogicCoroutine());
        }

        public override void Die()
        {
            ScoreManager.AddScore(50);
            gameObject.SetActive(false);

            if (PowerUpDropper.IsDrop())
                PowerUpDropper.Drop(_transform.position);
        }

        public override void TakeDamage(int dmg)
        {
            Health -= dmg;

            if (Health <= 0) Die();
        }
        #endregion

        void AimOnTarget()
        {
            var dir = (_target.position - transform.position).normalized;
            float rotZ = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg - 90f;
            _transform.rotation = Quaternion.Euler(Vector3.forward * rotZ);

            Debug.DrawRay(_transform.position, dir, Color.blue);
        }

        IEnumerator ShootLogicCoroutine()
        {
            while (true)
            {
                yield return new WaitForSeconds(shootDelay);
                Shoot();
            }
        }

        void Shoot()
        {
            ProjectileCreator.CreateProjectile(
                projPrefab,
                _transform.position,
                _transform.rotation,
                1,
                0f);
        }
    }
}