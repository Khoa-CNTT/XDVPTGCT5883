using System;
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
        public static Action OnWaveCompleted;

        [Header("Settings")]
        [SerializeField] private SpawnModes spawnMode = SpawnModes.Fixed;
        [SerializeField] private int enemyCount = 10;
        [SerializeField] private float delayBtwWaves = 1f;

        [Header("Fixed Mode")]
        [SerializeField] private float delayBtwSpawns;

        [Header("Random Mode")]
        [SerializeField] private float minRandomDelay;
        [SerializeField] private float maxRandomDelay;

        [Header("Poller")]
        [SerializeField] private ObjectPooling enemyWave10Pooler;
        [SerializeField] private ObjectPooling enemyWave11To20Pooler;
        [SerializeField] private ObjectPooling enemyWave21To30Pooler;
        [SerializeField] private ObjectPooling enemyWave31To40Pooler;
        [SerializeField] private ObjectPooling enemyWave41To50Pooler;


        private float spawnerTimer;
        private int enemySpawned;
        private int enemyRemaining;
        private Waypoint _waypoint;

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

        void Start()
        {
            _waypoint = GetComponent<Waypoint>();

            enemyRemaining = enemyCount;
        }

        void Update()
        {
            spawnerTimer -= Time.deltaTime;

            if (LevelManager.Instance != null && LevelManager.Instance.IsFinalWave)
            {
                return;
            }

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
            GameObject newInstance = GetPooler().GetInstanceFromPool();
            EnemiesController enemiesController = newInstance.GetComponent<EnemiesController>();
            EnemyHealth enemyHealth = newInstance.GetComponent<EnemyHealth>();
            enemiesController.ResetEnemy();
            enemiesController.waypoint = _waypoint;
            enemiesController.transform.localPosition = transform.position;
            newInstance.SetActive(true);
            enemyHealth.ResetHealth();
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
            float radomTimer = UnityEngine.Random.Range(minRandomDelay, maxRandomDelay);
            return radomTimer;
        }

        private ObjectPooling GetPooler()
        {
            int currentWave = LevelManager.Instance.CurrentWave;
            if (currentWave <= 10)
            {
                return enemyWave10Pooler;
            }

            if (currentWave > 10 && currentWave <= 20)
            {
                return enemyWave11To20Pooler;
            }

            if (currentWave > 20 && currentWave <= 30)
            {
                return enemyWave21To30Pooler;
            }

            if (currentWave > 30 && currentWave <= 40)
            {
                return enemyWave31To40Pooler;
            }

            if (currentWave > 40 && currentWave <= 50)
            {
                return enemyWave41To50Pooler;
            }

            return null;
        }

        private IEnumerator NextWave()
        {
            yield return new WaitForSeconds(delayBtwWaves);
            enemyRemaining = enemyCount;
            spawnerTimer = 0f;
            enemySpawned = 0;
        }

        private void RecordEnemy(EnemiesController dummy)
        {
            enemyRemaining--;
            if (enemyRemaining <= 0)
            {
                OnWaveCompleted?.Invoke();
                StartCoroutine(NextWave());
            }
        }
    }
}
