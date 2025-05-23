using System.Collections.Generic;
using UnityEngine;

public class Quadtree
{
    public Rect boundary;
    public int capacity;
    public List<Vector2> points;
    public bool divided = false;

    public Quadtree northeast, northwest, southeast, southwest;

    public Quadtree(Rect boundary, int capacity)
    {
        this.boundary = boundary;
        this.capacity = capacity;
        this.points = new List<Vector2>();
        this.divided = false;
    }

    public bool Insert(Vector2 point)
    {
        if (!boundary.Contains(point))
            return false;

        if (points.Count < capacity)
        {
            points.Add(point);
            return true;
        }
        else
        {
            if (!divided)
                Subdivide();

            if (northeast.Insert(point)) return true;
            if (northwest.Insert(point)) return true;
            if (southeast.Insert(point)) return true;
            if (southwest.Insert(point)) return true;
        }

        return false;
    }

    private void Subdivide()
    {
        float x = boundary.center.x;
        float y = boundary.center.y;
        float w = boundary.width / 2f;
        float h = boundary.height / 2f;

        northeast = new Quadtree(new Rect(x, y - h, w, h), capacity);
        northwest = new Quadtree(new Rect(x - w, y - h, w, h), capacity);
        southeast = new Quadtree(new Rect(x, y, w, h), capacity);
        southwest = new Quadtree(new Rect(x - w, y, w, h), capacity);

        divided = true;
    }

    public void Query(Rect range, List<Vector2> found)
    {
        if (!boundary.Overlaps(range))
            return;

        foreach (var point in points)
        {
            if (range.Contains(point))
                found.Add(point);
        }

        if (divided)
        {
            northwest.Query(range, found);
            northeast.Query(range, found);
            southwest.Query(range, found);
            southeast.Query(range, found);
        }
    }

    // public void ShowGizmos()
    // {
    //     Gizmos.color = new Color(1f, 1f, 1f, 0.4f);
    //     Gizmos.DrawWireCube(boundary.center, boundary.size);

    //     if (divided)
    //     {
    //         northeast.ShowGizmos();
    //         northwest.ShowGizmos();
    //         southeast.ShowGizmos();
    //         southwest.ShowGizmos();
    //     }
    // }

    public void DrawDebug()
    {
        Vector2 center = boundary.center;
        Vector2 size = boundary.size;

        Vector3 topLeft = new Vector3(center.x - size.x / 2, center.y + size.y / 2, 0);
        Vector3 topRight = new Vector3(center.x + size.x / 2, center.y + size.y / 2, 0);
        Vector3 bottomLeft = new Vector3(center.x - size.x / 2, center.y - size.y / 2, 0);
        Vector3 bottomRight = new Vector3(center.x + size.x / 2, center.y - size.y / 2, 0);

        Debug.DrawLine(topLeft, topRight, Color.white);
        Debug.DrawLine(topRight, bottomRight, Color.white);
        Debug.DrawLine(bottomRight, bottomLeft, Color.white);
        Debug.DrawLine(bottomLeft, topLeft, Color.white);

        if (divided)
        {
            northeast.DrawDebug();
            northwest.DrawDebug();
            southeast.DrawDebug();
            southwest.DrawDebug();
        }
    }

}
