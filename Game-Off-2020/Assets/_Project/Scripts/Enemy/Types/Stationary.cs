using SpaceGame.Core;
using System.Collections;
using UnityEngine;

namespace SpaceGame.EnemyLogic
{
    public class Stationary : Enemy
    {
        [Header("Turret")]
        [SerializeField] private GameObject projPrefab = default;
        [SerializeField] private Transform turretRig = default;
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

        public override void OnSpawn()
        {
            base.OnSpawn();
            RestoreHealth();
            StartCoroutine(ShootLogicCoroutine());
        }

        public override void Die()
        {
            base.Die();
            ScoreManager.AddScore(50);
        }

        void AimOnTarget()
        {
            var dir = (_target.position - _transform.position).normalized;
            float rotZ = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg + 90f;
            turretRig.rotation = Quaternion.Euler(Vector3.forward * rotZ);
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
            var turret = turretRig.transform.GetChild(0);
            var position = turret.position;

            var dir = (_target.position - _transform.position).normalized;
            var rotZ = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg - 90f;
            var rotation = Quaternion.Euler(Vector3.forward * rotZ);

            Effects.VFX.VFXManager.Instance.Emit("Enemy_Shoot", position, rotation);
            Effects.Audio.AudioManager.Instance.PlaySFX("Enemy_Shoot");

            ProjectileCreator.CreateProjectile(
                projPrefab,
                position,
                rotation,
                1,
                0f);
        }
    }
}