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
        [SerializeField] private SpawnModes spawnMode = SpawnModes.Fixed;
        [SerializeField] private int enemyCount = 10;
        [SerializeField] private GameObject testGameobject;
        [SerializeField] private float delayBtwSpawns;
        [SerializeField] private float minRandomDelay;
        [SerializeField] private float maxRandomDelay;

        private float spawnerTimer;
        private int enemySpawned;

        void Update()
        {
            spawnerTimer -= Time.deltaTime;
            if (spawnerTimer <= 0)
            {
                spawnerTimer = GetRandomDelay();
                if (enemySpawned < enemyCount)
                {
                    enemySpawned++;
                    SpawnEnemy();
                }
            }
        }

        private void SpawnEnemy()
        {
            Instantiate(testGameobject, transform.position, Quaternion.identity);
        }

        private float GetRandomDelay()
        {
            float radomTimer = Random.Range(minRandomDelay, maxRandomDelay);
            return radomTimer;
        }
    }
}
