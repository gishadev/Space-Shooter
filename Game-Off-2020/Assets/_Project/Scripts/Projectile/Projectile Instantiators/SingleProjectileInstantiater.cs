using SpaceGame.Optimisation;
using UnityEngine;

namespace SpaceGame.Projectile
{
    public class SingleProjectileInstantiater : ProjectileInstantiator
    {
        private GameObject prefab;
        private Vector3 position;
        private Quaternion rotation;

        public SingleProjectileInstantiater(
            GameObject _prefab, Vector3 _position, Quaternion _rotation)
        {
            prefab = _prefab;
            position = _position;
            rotation = _rotation;
        }

        public override void Instantiate()
        {
            PoolManager.Instantiate(prefab, position, rotation);
        }
    }
}