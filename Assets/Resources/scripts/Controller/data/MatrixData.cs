using NorskaLib.Spreadsheets;
using System;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;
using static MatrixData;
[CreateAssetMenu(fileName = "MatrixData", menuName = "MatrixData")]
public class MatrixData : SpreadsheetsContainerBase
{
    [SpreadsheetContent]
    [SerializeField] listMatric content;
    public listMatric Conten => content;
    public List<List<Level>> GetAllMatric()
    {
        List<List<Level>> allmatric = new List<List<Level>>();
        FieldInfo[] fields = typeof(listMatric).GetFields(BindingFlags.Public | BindingFlags.Instance);
        foreach (FieldInfo field in fields)
        {
            if (field.FieldType == typeof(List<Level>))
            {
                List<Level> levelList = (List<Level>)field.GetValue(content);
                if (levelList != null)
                {
                    allmatric.Add(levelList);
                }
            }
        }
        return allmatric;
    }

    [Serializable]
    public class Level
    {
        public string Colum1;
        public string Colum2;
        public string Colum3;
        public string Colum4;
        public string Colum5;
        public string Colum6;
        public string Colum7;
        public string Colum8;
        public string Colum9;
        public string Colum10;
        public List<string> GetAllColums()
        {
            return new List<string>
            {
                Colum1,Colum2, Colum3, Colum4,Colum5, Colum6, Colum7, Colum8,Colum9,Colum10
            };
        }
    }
    [Serializable]
    public class listMatric
    {
        [SpreadsheetPage("Level_1")]
        public List<Level> lv1;
    }
}
