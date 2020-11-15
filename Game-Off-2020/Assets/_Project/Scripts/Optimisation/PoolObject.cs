using UnityEngine;

namespace SpaceGame.Optimisation
{
    [System.Serializable]
    public class PoolObject
    {
        [SerializeField] private GameObject prefab = default;
        [SerializeField] private int initCount = default;

        public GameObject Prefab => prefab;
        public string Name => prefab.name;
        public int OnInitCount => initCount;
        public int InstanceId => prefab.GetInstanceID();
    }
}