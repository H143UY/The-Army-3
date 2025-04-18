using System.Collections.Generic;
using UnityEngine;

public class QuadTree<T>
{
    private Rect bounds;
    private int capacity;
    private List<QuadTreeNode<T>> points;
    private bool divided;

    private QuadTree<T> northeast, northwest, southeast, southwest;

    public QuadTree(Rect bounds, int capacity)
    {
        this.bounds = bounds;
        this.capacity = capacity;
        this.points = new List<QuadTreeNode<T>>();
        this.divided = false;
    }

    public bool Insert(Vector2 position, T data)
    {
        if (!bounds.Contains(position)) return false;

        if (points.Count < capacity)
        {
            points.Add(new QuadTreeNode<T>(position, data));
            return true;
        }
        else
        {
            if (!divided) Subdivide();

            return northeast.Insert(position, data)
                || northwest.Insert(position, data)
                || southeast.Insert(position, data)
                || southwest.Insert(position, data);
        }
    }

    private void Subdivide()
    {
        float x = bounds.x;
        float y = bounds.y;
        float w = bounds.width / 2f;
        float h = bounds.height / 2f;

        northeast = new QuadTree<T>(new Rect(x + w, y, w, h), capacity);
        northwest = new QuadTree<T>(new Rect(x, y, w, h), capacity);
        southeast = new QuadTree<T>(new Rect(x + w, y + h, w, h), capacity);
        southwest = new QuadTree<T>(new Rect(x, y + h, w, h), capacity);

        divided = true;
    }

    public List<T> Query(Rect range)
    {
        List<T> found = new List<T>();

        if (!bounds.Overlaps(range)) return found;

        foreach (var p in points)
        {
            if (range.Contains(p.Position)) found.Add(p.Data);
        }

        if (divided)
        {
            found.AddRange(northeast.Query(range));
            found.AddRange(northwest.Query(range));
            found.AddRange(southeast.Query(range));
            found.AddRange(southwest.Query(range));
        }

        return found;
    }
}