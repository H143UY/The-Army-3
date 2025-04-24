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
    private QuadTree<GameObject> quadTree;
    [SerializeField] private GameObject[] enemyPrefabs;
    void Awake()
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
            List<string> columns = row.GetAllColums(); // 👉 Lấy tất cả cột trước

            for (int x = 0; x < columns.Count; x++)
            {
                string rawValue = columns[x]; // 👉 Không cần GetColumnValue nữa
                string cellValue = CleanValue(rawValue);

                if (cellValue.StartsWith("e"))
                {
                    string numberPart = cellValue.Substring(1);

                    if (int.TryParse(numberPart, out int enemyIndex) && enemyIndex < enemyPrefabs.Length)
                    {
                        GameObject prefab = enemyPrefabs[enemyIndex];
                        GameObject enemy = SmartPool.Instance.Spawn(prefab, rootTileParent.position, rootTileParent.rotation);
                        enemy.transform.SetParent(rootTileParent, false);
                        enemy.transform.localPosition = new Vector3(x * spacing, -y * spacing, 0);
                        enemy.name = $"Enemy_{enemyIndex}";

                        InforData enemyInfo = dataEnemy.GetEnemyInfoByID(cellValue);
                        if (enemyInfo != null)
                        {
                            EnemyController enemyController = enemy.GetComponent<EnemyController>();
                            if (enemyController != null && int.TryParse(enemyInfo.Health, out int health))
                            {
                                enemyController.SetHp(health);
                            }
                        }
                    }
                    else
                    {
                        Debug.LogWarning($"Không tìm thấy enemy prefab cho {cellValue}");
                    }
                }
                else
                {
                    GameObject tile = SmartPool.Instance.Spawn(tilePrefab, Vector3.zero, Quaternion.identity);
                    tile.transform.SetParent(rootTileParent, false);
                    tile.transform.localPosition = new Vector3(x * spacing, -y * spacing, 0);

                    TileData tileData = tile.GetComponent<TileData>();
                    if (tileData != null && int.TryParse(cellValue, out int HpMatrix))
                    {
                        tileData.position = new Vector2Int(x, y);
                        tileData.SetHp(HpMatrix);
                    }

                    quadTree.Insert(tile.transform.position, tile);
                }
            }
        }
    }
    string CleanValue(string raw)
    {
        return raw.StartsWith("#") ? raw.Substring(1) : raw;
    }

}
