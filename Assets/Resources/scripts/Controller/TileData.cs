using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TileData : MonoBehaviour
{
    public Vector2Int position;
    public Text TextHp;
    public TileData(int x, int y)
    {
        this.position = new Vector2Int(x, y);
    }
    public Rect GetBounds(float tileSize)
    {
        return new Rect(position.x * tileSize, position.y * tileSize, tileSize, tileSize);
    }
    public void SetHp(string hp)
    {
        TextHp.text = hp;    
    }
}
