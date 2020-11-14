using UnityEngine;

namespace SpaceGame.Projectile
{
    public class Projectile : MonoBehaviour
    {
        [SerializeField] private float flySpeed;
        [SerializeField] private float lifeTime;

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
        }

        private void OneDirectionMovement()
        {
            Vector3 dir = _transform.up;
            _transform.Translate(dir * flySpeed * Time.deltaTime, Space.World);
        }

        private void DestroyProjectile()
        {
            gameObject.SetActive(false);
        }

    }
}