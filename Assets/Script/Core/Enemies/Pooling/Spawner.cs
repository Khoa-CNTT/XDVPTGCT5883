using System.Collections;
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
        [SerializeField] private float delayBtwWaves = 1f;

        [Header("Fixed Mode")]
        [SerializeField] private float delayBtwSpawns;

        [Header("Random Mode")]
        [SerializeField] private float minRandomDelay;
        [SerializeField] private float maxRandomDelay;

        private float spawnerTimer;
        private int enemySpawned;
        private int enemyRemaining;

        private ObjectPooling pool;
        private Waypoint _waypoint;

        void Start()
        {
            pool = GetComponent<ObjectPooling>();
            _waypoint = GetComponent<Waypoint>();

            enemyRemaining = enemyCount;
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
            EnemiesController enemiesController = newInstance.GetComponent<EnemiesController>();
            enemiesController.ResetEnemy();
            enemiesController.waypoint = _waypoint;
            enemiesController.transform.localPosition = transform.position;
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

        private IEnumerator NextWave()
        {
            yield return new WaitForSeconds(delayBtwWaves);
            enemyRemaining = enemyCount;
            spawnerTimer = 0f;
            enemySpawned = 0;
        }

        private void RecordEnemy()
        {
            enemyRemaining--;
            if (enemyRemaining <= 0)
            {
                StartCoroutine(NextWave());
            }
        }

        void OnEnable()
        {
            EnemiesController.OnEndReached += RecordEnemy;
            EnemyHealth.OnEnemyKilled += RecordEnemy;
        }

        void OnDisable()
        {
            EnemiesController.OnEndReached -= RecordEnemy;
            EnemyHealth.OnEnemyKilled -= RecordEnemy;
        }
    }
}
