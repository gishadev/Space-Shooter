using SpaceGame.Optimisation;
using SpaceGame.World;
using System.Collections;
using UnityEngine;

namespace SpaceGame.EnemyNamespace
{
    public class EnemySpawner : MonoBehaviour
    {
        [Header("Data")]
        [SerializeField] private EnemySpawnData[] enemiesToSpawn = default;
        [Header("Timings")]
        [SerializeField] private float checkDelayInSeconds = default;
        [SerializeField] private float spawnTimeInSeconds = default;

        private void Start()
        {
            StartCoroutine(SpawnCheckCoroutine());
        }

        private void OnValidate()
        {
            if (checkDelayInSeconds < 0) checkDelayInSeconds = 0;
            if (spawnTimeInSeconds <= 0) spawnTimeInSeconds = 0.01f;
        }

        IEnumerator SpawnCheckCoroutine()
        {
            while (true)
            {
                yield return StartCoroutine(SpawningCoroutine());
                yield return new WaitForSeconds(checkDelayInSeconds);
            }
        }

        IEnumerator SpawningCoroutine()
        {
            GameObject spawnedEnemy = SpawnEnemy();

            float progress = 0;

            Vector3 startScale = spawnedEnemy.transform.localScale;
            SpriteRenderer sr = spawnedEnemy.GetComponent<SpriteRenderer>();
            Color color = new Color(1f, 1f, 1f, 0f);

            while (progress < 1f)
            {
                progress += Time.deltaTime / spawnTimeInSeconds;

                spawnedEnemy.transform.localScale = startScale * progress;

                color.a = progress;
                sr.color = color;

                yield return null;
            }

            spawnedEnemy.GetComponent<Enemy>().Init();
        }

        GameObject SpawnEnemy()
        {
            var data = enemiesToSpawn[Random.Range(0, enemiesToSpawn.Length)];

            GameObject prefab = data.prefab;
            Vector2 position = new Vector2(
                Random.Range(WorldBounds.MaxX, -WorldBounds.MaxX),
                Random.Range(WorldBounds.MaxY, -WorldBounds.MaxY));
            Quaternion rotation = prefab.transform.rotation;

            return PoolManager.Instantiate(prefab, position, rotation);
        }
    }
}