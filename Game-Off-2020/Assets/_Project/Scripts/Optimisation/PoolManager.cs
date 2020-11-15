using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace SpaceGame.Optimisation
{
    public static class PoolManager
    {
        private static Dictionary<PoolObject, List<GameObject>> objectsByPoolObject;
        private static Dictionary<PoolObject, Transform> parentByPoolObject;

        private static List<PoolObject> poolObjects;

        public static void Init(List<PoolObject> _poolObjects)
        {
            objectsByPoolObject = new Dictionary<PoolObject, List<GameObject>>();
            parentByPoolObject = new Dictionary<PoolObject, Transform>();

            poolObjects = _poolObjects;
            PoolManager.InitializePools(poolObjects);
        }

        public static GameObject Instantiate(GameObject prefab, Vector3 position, Quaternion rotation)
        {
            PoolObject po = GetPoolObjectFromPrefab(prefab);

            List<GameObject> sceneObjectsList;
            if (objectsByPoolObject.TryGetValue(po, out sceneObjectsList))
            {
                if (sceneObjectsList.Any(x => !x.activeInHierarchy))
                    return ActivateAvailableObject(position, rotation, sceneObjectsList);
            }

            else
            {
                objectsByPoolObject.Add(po, new List<GameObject>());
                CreateParent(po);
            }

            return InstantiateNewObject(prefab, position, rotation, po);
        }

        private static void InitializePools(List<PoolObject> _poolObjects)
        {
            for (int i = 0; i < _poolObjects.Count; i++)
            {
                PoolObject po = _poolObjects[i];

                objectsByPoolObject.Add(po, new List<GameObject>());
                CreateParent(po);

                for (int j = 0; j < po.OnInitCount; j++)
                    InstantiateNewObject(po.Prefab, Vector3.zero, Quaternion.identity, po).SetActive(false);
            }
        }

        #region Private Methods
        private static GameObject InstantiateNewObject(GameObject prefab, Vector3 position, Quaternion rotation, PoolObject po)
        {
            Transform parent = parentByPoolObject[po];

            GameObject createdObject = Object.Instantiate(prefab, position, rotation, parent);
            objectsByPoolObject[po].Add(createdObject);

            return createdObject;
        }

        private static GameObject ActivateAvailableObject(Vector3 position, Quaternion rotation, List<GameObject> sceneObjectsList)
        {
            GameObject firstActiveObject = sceneObjectsList.FirstOrDefault(x => !x.activeInHierarchy);

            firstActiveObject.transform.position = position;
            firstActiveObject.transform.rotation = rotation;

            firstActiveObject.SetActive(true);

            return firstActiveObject;
        }

        private static PoolObject GetPoolObjectFromPrefab(GameObject prefab)
        {
            int prefabId = prefab.GetInstanceID();
            int index = poolObjects.FindIndex(x => x.InstanceId.Equals(prefabId));
            if (index == -1)
            {
                Debug.LogError("Prefab wasn't found in poolObjects collection.");
                return null;
            }

            return poolObjects[index];
        }

        private static void CreateParent(PoolObject poKey)
        {
            string name = string.Format("pool_{0}", poKey.Name);
            GameObject parent = new GameObject(name);

            parentByPoolObject.Add(poKey, parent.transform);
        }
        #endregion
    }
}