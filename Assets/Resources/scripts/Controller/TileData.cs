using Core.Pool;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TileData : MonoBehaviour
{
    public Vector2Int position;
    public Text TextHp;
    private int HpMatrix;
    public TileData(int x, int y)
    {
        this.position = new Vector2Int(x, y);
    }
    private void Update()
    {
        if(HpMatrix <= 0)
        {
            SmartPool.Instance.Despawn(this.gameObject);
        }
        TextHp.text = HpMatrix.ToString();
    }
    public Rect GetBounds(float tileSize)
    {
        return new Rect(position.x * tileSize, position.y * tileSize, tileSize, tileSize);
    }
    public void SetHp(int hp)
    {
        HpMatrix = hp;
    }
    public void TakeDamage(int Damage)
    {
        HpMatrix -= Damage;
    }
}
