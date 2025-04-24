using NorskaLib.Spreadsheets;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using UnityEngine;
[CreateAssetMenu(menuName = "PlayerData", fileName = "PlayerData")]
public class PlayerData : SpreadsheetsContainerBase
{
    [SpreadsheetContent]
    [SerializeField] listInforPlayer content;
    public listInforPlayer ContentContent => content;
    //public InforData GetEnemyInfoByID(string id)
    //{
    //    foreach (var info in content.Data_Player)
    //    {
    //        if (info.ID == id)
    //            return info;
    //    }
    //    return null;
    //}
}
[Serializable]

public class InforPlayerData
{
    public int HP_Level;
    public string HP;
    public int Attack_Level;
    public string Attack_Speed;
    public int PowerLevel;
    public string Power;
}

[Serializable]
public class listInforPlayer
{
    [SpreadsheetPage("Data_Player")]
    public List<InforPlayerData> Data_Player;
}
