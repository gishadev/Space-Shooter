using SpaceGame.Optimisation;
using UnityEngine;

namespace SpaceGame.Core
{
    public static class ProjectileCreator
    {
        public static void CreateProjectile(GameObject prefab, Vector3 position, Quaternion rotation, int projCount, float spreading)
        {
            float zStartOffset = rotation.eulerAngles.z - spreading * projCount / 2;
            Quaternion startRot = Quaternion.Euler(Vector3.forward * zStartOffset);

            for (int i = 0; i < projCount; i++)
            {
                float zProjOffset = startRot.eulerAngles.z + spreading * i;
                Quaternion newRot = Quaternion.Euler(Vector3.forward * zProjOffset);

                PoolManager.Instantiate(prefab, position, newRot);
            }
        }
    }
}