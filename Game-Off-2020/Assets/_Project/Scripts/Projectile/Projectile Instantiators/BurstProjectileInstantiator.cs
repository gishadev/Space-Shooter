using SpaceGame.Optimisation;
using UnityEngine;

namespace SpaceGame.Projectile
{
    public class BurstProjectileInstantiator : ProjectileInstantiator
    {
        private readonly GameObject prefab;
        private readonly Vector3 position;
        private readonly Quaternion rotation;
        private readonly int projCount;
        private readonly float spreading;

        public BurstProjectileInstantiator(
            GameObject _prefab, Vector3 _position, Quaternion _rotation, int _projCount, float _spreading)
        {
            prefab = _prefab;
            position = _position;
            rotation = _rotation;
            projCount = _projCount;
            spreading = _spreading;
        }

        public override void Instantiate()
        {
            float zStartOffset = rotation.eulerAngles.z - spreading * projCount / 2;
            Quaternion startRot = Quaternion.Euler(Vector3.forward * zStartOffset);

            for (int i = 0; i < projCount; i++)
            {
                float zProjOffset = startRot.eulerAngles.z + spreading * i;
                Quaternion newRot = Quaternion.Euler(Vector3.forward * zProjOffset);

                ObjectPooler.Instantiate(prefab, position, newRot);
            }
        }
    }
}