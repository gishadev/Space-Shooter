using UnityEngine;

namespace SpaceGame.Projectile
{
    public class Projectile : MonoBehaviour
    {
        [Header("General")]
        [SerializeField] private float flySpeed = default;
        [SerializeField] private float lifeTime = default;
        [Header("Raycast")]
        [SerializeField] private int damage = default;
        [SerializeField] private float rayLength = default;
        [Header("Layer Mask")]
        [SerializeField] private LayerMask whatIsSolid = default;

        public int Damage => damage;

        bool _isInit = false;

        Transform _transform;


        private void Awake()
        {
            _transform = transform;
        }

        private void OnEnable()
        {
            if (!_isInit)
            {
                _isInit = true;
                return;
            }

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

        public virtual void OnHit(RaycastHit2D hitInfo)
        { }

        public virtual void DestroyProjectile()
        {
            CancelInvoke();
            gameObject.SetActive(false);
        }
    }
}