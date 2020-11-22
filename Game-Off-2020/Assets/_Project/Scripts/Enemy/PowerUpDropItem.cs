using UnityEngine;

namespace SpaceGame.EnemyLogic
{
    [System.Serializable]
    internal class PowerUpDropItem
    {
        [SerializeField] private GameObject powerUpPrefab = default;
        [SerializeField] private int entries = default;

        public GameObject Prefab => powerUpPrefab;
        public int Entries => entries;

        public PowerUpDropItem(GameObject _prefab)
        {
            powerUpPrefab = _prefab;
        }
    }
}