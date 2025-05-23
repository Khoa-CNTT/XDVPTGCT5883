using System.Collections.Generic;
using UnityEngine;

public class test2 : MonoBehaviour
{
    public GameObject pointPrefab;
    public int capacity = 4;

    private Quadtree tree;
    private Rect treeArea;

    void Start()
    {
        // Tính toán vùng theo camera
        Camera cam = Camera.main;
        float height = 2f * cam.orthographicSize;
        float width = height * cam.aspect;
        Vector2 center = cam.transform.position;

        treeArea = new Rect(
            center.x - width / 2f,
            center.y - height / 2f,
            width,
            height
        );

        tree = new Quadtree(treeArea, capacity);
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Vector3 worldPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            Vector2 point = new Vector2(worldPos.x, worldPos.y);

            if (treeArea.Contains(point))
            {
                bool inserted = tree.Insert(point);
                if (inserted && pointPrefab != null)
                {
                    Instantiate(pointPrefab, new Vector3(point.x, point.y, 0), Quaternion.identity);
                }
            }
        }

        if (tree != null)
            tree.DrawDebug();
    }

    // void OnDrawGizmos()
    // {
    //     if (tree != null)
    //     {
    //         tree.DrawDebug();
    //     }
    // }
}
