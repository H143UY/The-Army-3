using NorskaLib.Spreadsheets;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using UnityEngine;
using static DataEnemy;
[CreateAssetMenu(menuName = "DataEnemy", fileName = "DataEnemy")]
public class DataEnemy : SpreadsheetsContainerBase
{
    [SpreadsheetContent]
    [SerializeField] listInfor content;
    public listInfor ContentContent => content;
    public InforData GetEnemyInfoByID(string id)
    {
        foreach (var info in content.enemy_data)
        {
            if (info.ID == id)
                return info;
        }
        return null;
    }
}
[Serializable]

public class InforData
{
    public string ID;
    public string Health;
    public string Attack_Speed;
}

[Serializable]
public class listInfor
{
    [SpreadsheetPage("enemy_data")]
    public List<InforData> enemy_data;
}
