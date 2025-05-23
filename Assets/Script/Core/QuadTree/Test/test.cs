using System.Collections.Generic;
using UnityEngine;

public class QuadtreeManager : MonoBehaviour
{
    public Rect area = new Rect(-50, -50, 100, 100);
    public int capacity = 4;

    public Quadtree tree;

    void Awake()
    {
        tree = new Quadtree(area, capacity);
    }

    public void Register(Vector2 point)
    {
        tree.Insert(point);
    }

    void OnDrawGizmos()
    {
        if (tree != null)
        {
            tree.DrawDebug();
        }
    }
}
