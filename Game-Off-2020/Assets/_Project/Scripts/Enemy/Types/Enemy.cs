using SpaceGame.Optimisation;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using UnityEngine;

namespace SpaceGame.EnemyLogic
{
    public abstract class Enemy : MonoBehaviour
    {
        [Header("General")]
        [SerializeField] private int maxHealth = default;

        [Header("Dropping Power-Ups")]
        [SerializeField] private PowerUpDropper powerUpDropper = default;

        public PowerUpDropper PowerUpDropper => powerUpDropper;

        public int Health
        {
            get { return _health; }
            set { _health = Mathf.Clamp(value, 0, maxHealth); }
        }
        int _health;

        Collider2D _collider;

        private void Awake()
        {
            if (_collider == null) _collider = GetComponent<Collider2D>();
        }

        private void OnValidate()
        {
            powerUpDropper.UpdateDropItems();
        }

        public void RestoreHealth()
        {
            Health = maxHealth;
        }

        public virtual void OnStartSpawning()
        {
            if (_collider == null) _collider = GetComponent<Collider2D>();

            _collider.enabled = false;
        }

        public virtual void OnSpawn()
        {
            _collider.enabled = true;
        }

        public abstract void TakeDamage(int dmg);
        public abstract void Die();
    }

    [System.Serializable]
    public class PowerUpDropper
    {
        [Header("Power-Up Dropping")]
        [SerializeField] private float dropChance = default;
        [SerializeField] private List<PowerUpDropItem> dropItems = new List<PowerUpDropItem>();

        private const string FolderPath = "Assets/_Project/Prefabs/PowerUps";

        public void Drop(Vector3 pos) => PoolManager.Instantiate(GetRandomPowerUp(), pos, Quaternion.identity);
        public bool IsDrop() => Random.Range(0f, 1f) < dropChance;

        public GameObject GetRandomPowerUp()
        {
            int[] allEntries = dropItems.Select(x => x.Entries).ToArray();
            int maxEntry = allEntries.Sum();
            int randomEntry = Random.Range(0, maxEntry);

            for (int i = 0, leftPoint = 0; i < allEntries.Length; i++)
            {
                int rightPoint = leftPoint + allEntries[i];
                if (randomEntry >= leftPoint && randomEntry <= rightPoint) return dropItems[i].Prefab;

                leftPoint += allEntries[i];
            }

            Debug.LogError("Prefab wasn't found!");
            return null;
        }

        public void UpdateDropItems()
        {
            string[] pathes = Directory.GetFiles(FolderPath);

            if (dropItems.Count == pathes.Length)
                return;

            foreach (string path in pathes)
            {
                if (path.Contains(".meta")) continue;

                Object prefabObject = UnityEditor.AssetDatabase.LoadAssetAtPath(path, typeof(GameObject));

                GameObject prefab = prefabObject as GameObject;
                if (!dropItems.Select(x => x.Prefab).Contains(prefab))
                    dropItems.Add(new PowerUpDropItem(prefab));
            }
        }
    }

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