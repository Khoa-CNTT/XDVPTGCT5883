using UnityEngine;

public enum SpawnModes
{
    Fixed,
    Random
}

namespace dang
{
    public class Spawner : MonoBehaviour
    {
        [Header("Settings")]
        [SerializeField] private SpawnModes spawnMode = SpawnModes.Fixed;
        [SerializeField] private int enemyCount = 10;

        [Header("Fixed Mode")]
        [SerializeField] private float delayBtwSpawns;

        [Header("Random Mode")]
        [SerializeField] private float minRandomDelay;
        [SerializeField] private float maxRandomDelay;

        private float spawnerTimer;
        private int enemySpawned;

        private ObjectPooling pool;

        void Start()
        {
            pool = GetComponent<ObjectPooling>();
        }

        void Update()
        {
            spawnerTimer -= Time.deltaTime;
            if (spawnerTimer <= 0)
            {
                spawnerTimer = GetSpawnDelay();
                if (enemySpawned < enemyCount)
                {
                    enemySpawned++;
                    SpawnEnemy();
                }
            }
        }

        private void SpawnEnemy()
        {
            GameObject newInstance = pool.GetInstanceFromPool();
            newInstance.SetActive(true);
        }

        private float GetSpawnDelay()
        {
            float delay = 0;
            if (spawnMode == SpawnModes.Fixed)
            {
                delay = delayBtwSpawns;
            }
            else if (spawnMode == SpawnModes.Random)
            {
                delay = GetRandomDelay();
            }

            return delay;
        }

        private float GetRandomDelay()
        {
            float radomTimer = Random.Range(minRandomDelay, maxRandomDelay);
            return radomTimer;
        }
    }
}
