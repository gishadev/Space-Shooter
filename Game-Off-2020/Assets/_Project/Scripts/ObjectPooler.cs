using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public static class ObjectPooler
{
    private static Dictionary<int, List<GameObject>> objectsByPrefabId;
    private static Dictionary<int, Transform> parentByPrefabId;

    public static void Instantiate(GameObject prefab, Vector3 position, Quaternion rotation)
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

                return;
            }
        }
        else CreateParent(string.Format("pool_{0}", prefab.name), prefabId);


        ////////////////////////////////////// New Object Creating //////////////////////////////////////
        ///
        Transform parent = parentByPrefabId[prefabId];
        GameObject createdObject = GameObject.Instantiate(prefab, position, rotation, parent);

        if (!objectsByPrefabId.ContainsKey(prefabId))
            objectsByPrefabId.Add(prefabId, new List<GameObject>());

        objectsByPrefabId[prefabId].Add(createdObject);
    }

    private static void CreateParent(string name, int prefabId)
    {
        GameObject parent = new GameObject(name);

        parentByPrefabId.Add(prefabId, parent.transform);
    }
}
