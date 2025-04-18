using Core.Pool;
using System.Collections.Generic;
using UnityEngine;
using static MatrixData;

public class DrawMatrixController : MonoBehaviour
{
    public MatrixData maxtricData;
    public DataEnemy dataEnemy;
    public GameObject tilePrefab;
    public Transform rootTileParent;
    public GameObject Enemy;
    private QuadTree<GameObject> quadTree;

    void Start()
    {
        Rect worldBounds = new Rect(0, 0, 32, 32);
        quadTree = new QuadTree<GameObject>(worldBounds, 4);

        SpawnMatrix();
    }

    void SpawnMatrix()
    {
        List<List<MatrixData.Level>> allLevels = maxtricData.GetAllMatric();
        List<MatrixData.Level> rows = allLevels[0];
        float spacing = 1f;
        for (int y = 0; y < rows.Count; y++)
        {
            var row = rows[y];
            for (int x = 0; x < 10; x++)
            {
                string rawValue = GetColumnValue(row, x);
                string cellValue = CleanValue(rawValue);
                if (cellValue != "")
                {
                    GameObject tile = SmartPool.Instance.Spawn(tilePrefab, Vector3.zero, Quaternion.identity);
                    tile.transform.SetParent(rootTileParent, false);
                    Vector3 localOffset = new Vector3(x * spacing, -y * spacing, 0);
                    tile.transform.localPosition = localOffset;

                    TileData tileData = tile.GetComponent<TileData>();
                    if (tileData != null)
                    {
                        tileData.position = new Vector2Int(x, y);
                        tileData.SetHp(cellValue);
                    }

                    tile.name = $"Tile_{x}_{y}";
                    quadTree.Insert(tile.transform.position, tile); 
                }
            }
        }
    }
    public static string CleanValue(string raw)
    {
        return raw.StartsWith("#") ? raw.Substring(1) : raw;
    }
    string GetColumnValue(MatrixData.Level data, int columnIndex)
    {
        return columnIndex switch
        {
            0 => data.Colum1,
            1 => data.Colum2,
            2 => data.Colum3,
            3 => data.Colum4,
            4 => data.Colum5,
            5 => data.Colum6,
            6 => data.Colum7,
            7 => data.Colum8,
            8 => data.Colum9,
            9 => data.Colum10,
            _ => "0"
        };
    }
}
