using UnityEngine;

public class QuadTreeNode<T>
{
    public Vector2 Position;
    public T Data;

    public QuadTreeNode(Vector2 position, T data)
    {
        Position = position;
        Data = data;
    }
}