using System.Collections.Generic;
using UnityEngine;

public class TestSpawner : MonoBehaviour
{
    public GameObject PrefabToSpawn;
    public int NumToSpawn = 10000;
    public GameObject SpawnZone;
    public float MinRadius = 0.5f;
    public float MaxRadius = 2f;

    void Start()
    {
        PerformSpawning();
    }

    void PerformSpawning()
    {
        if (SpawnZone == null || PrefabToSpawn == null) return;

        Bounds spawnBounds = SpawnZone.GetComponent<SpriteRenderer>().bounds;

        for (int i = 0; i < NumToSpawn; i++)
        {
            Vector3 spawnPos = new Vector3(
                Random.Range(spawnBounds.min.x, spawnBounds.max.x),
                Random.Range(spawnBounds.min.y, spawnBounds.max.y),
                0f);

            float radius = Random.Range(MinRadius, MaxRadius);

            GameObject go = Instantiate(PrefabToSpawn, spawnPos, Quaternion.identity);
            go.transform.localScale = new Vector3(radius, radius, 1f);
        }
    }
}
