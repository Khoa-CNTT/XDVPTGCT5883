using UnityEngine;

public class spawn : MonoBehaviour
{
    public GameObject prefab;
    public int amount = 20;
    public Vector2 spawnMin = new Vector2(-50, -50);
    public Vector2 spawnMax = new Vector2(50, 50);

    void Start()
    {
        for (int i = 0; i < amount; i++)
        {
            Vector2 pos = new Vector2(
                Random.Range(spawnMin.x, spawnMax.x),
                Random.Range(spawnMin.y, spawnMax.y)
            );
            Instantiate(prefab, pos, Quaternion.identity);
        }
    }
}
