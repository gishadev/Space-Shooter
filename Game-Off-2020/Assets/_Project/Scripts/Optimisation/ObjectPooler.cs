using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace SpaceGame.Optimisation
{
    public static class ObjectPooler
    {
        private static Dictionary<int, List<GameObject>> objectsByPrefabId;
        private static Dictionary<int, Transform> parentByPrefabId;

        public static GameObject Instantiate(GameObject prefab, Vector3 position, Quaternion rotation)
        {
            int prefabId = prefab.GetInstanceID();

            if (objectsByPrefabId == null) objectsByPrefabId = new Dictionary<int, List<GameObject>>();
            if (parentByPrefabId == null) parentByPrefabId = new Dictionary<int, Transform>();

            ////////////////////////////////////// Old Object Activating //////////////////////////////////////
            ///
            List<GameObject> objectsOfPrefabId;
            if (objectsByPrefabId.TryGetValue(prefabId, out objectsOfPrefabId))
            {
                if (objectsOfPrefabId.Any(x => !x.activeInHierarchy))
                {
                    GameObject firstActiveObject = objectsOfPrefabId.FirstOrDefault(x => !x.activeInHierarchy);

                    firstActiveObject.transform.position = position;
                    firstActiveObject.transform.rotation = rotation;

                    firstActiveObject.SetActive(true);

                    return firstActiveObject;
                }
            }
            else
            {
                objectsByPrefabId.Add(prefabId, new List<GameObject>());
                CreateParent(string.Format("pool_{0}", prefab.name), prefabId);
            }

            ////////////////////////////////////// New Object Creating //////////////////////////////////////
            ///
            Transform parent = parentByPrefabId[prefabId];
            
            GameObject createdObject = Object.Instantiate(prefab, position, rotation, parent);
            objectsByPrefabId[prefabId].Add(createdObject);

            return createdObject;
        }

        private static void CreateParent(string name, int prefabId)
        {
            GameObject parent = new GameObject(name);

            parentByPrefabId.Add(prefabId, parent.transform);
        }
    }
}