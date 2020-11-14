using SpaceGame.EnemyNamespace;
using UnityEngine;

namespace SpaceGame.Projectile
{
    public class Projectile : MonoBehaviour
    {
        [Header("General")]
        [SerializeField] private float flySpeed;
        [SerializeField] private float lifeTime;
        [Header("Raycast")]
        [SerializeField] private int damage;
        [SerializeField] private float rayLength;
        [Header("Layer Mask")]
        [SerializeField] private LayerMask whatIsSolid;


        Transform _transform;

        private void Awake()
        {
            _transform = transform;
        }

        private void OnEnable()
        {
            Invoke("DestroyProjectile", lifeTime);
        }

        private void Update()
        {
            OneDirectionMovement();
            Raycast();
        }

        void OneDirectionMovement()
        {
            Vector3 dir = _transform.up;
            _transform.Translate(dir * flySpeed * Time.deltaTime, Space.World);
        }

        void Raycast()
        {
            Vector3 origin = _transform.position - _transform.up * rayLength / 2f;
            Vector3 dir = _transform.up;
            RaycastHit2D hitInfo = Physics2D.Raycast(origin, dir, rayLength, whatIsSolid);

            Debug.DrawRay(origin, dir * rayLength, Color.blue);

            if (hitInfo.collider != null) OnHit(hitInfo);
        }

        void OnHit(RaycastHit2D hitInfo)
        {
            Enemy enemy;
            if (hitInfo.collider.TryGetComponent(out enemy))
                enemy.TakeDamage(damage);

            DestroyProjectile();
        }

        void DestroyProjectile() => gameObject.SetActive(false);
    }
}