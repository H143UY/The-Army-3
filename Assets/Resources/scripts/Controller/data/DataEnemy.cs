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

    public List<List<InforData>> GetAllColors()
    {
        List<List<InforData>> allColors = new List<List<InforData>>();
        FieldInfo[] fields = typeof(listInfor).GetFields(BindingFlags.Public | BindingFlags.Instance);
        foreach (FieldInfo field in fields)
        {
            if (field.FieldType == typeof(List<InforData>))
            {
                List<InforData> levelList = (List<InforData>)field.GetValue(content);
                if (levelList != null)
                {
                    allColors.Add(levelList);
                }
            }
        }
        return allColors;
    }
}
[Serializable]

public class InforData
{
    public string ID;
    public string Prefab;
    public string Health;
    public string Attack_Speed;
}

[Serializable]
public class listInfor
{
    [SpreadsheetPage("enemy_data")]
    public List<InforData> enemy_data;
}