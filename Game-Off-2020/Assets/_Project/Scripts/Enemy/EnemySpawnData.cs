using UnityEngine;

namespace SpaceGame.EnemyNamespace
{
    [CreateAssetMenu(fileName = "EnemySpawnData", menuName = "Scriptable Objects/Game/Create Enemy Spawn Data", order = 2)]
    public class EnemySpawnData : ScriptableObject
    {
        [Header("General")]
        public GameObject prefab;

        public string Name => prefab.name;

    }
}