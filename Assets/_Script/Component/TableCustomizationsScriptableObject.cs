using System;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "table_customizations", menuName = "ScriptableObjects/Table Customizations")]
public class TableCustomizationsScriptableObject : ScriptableObject
{
    [SerializeField] private List<TableCustomizationListElement> TableCustomizationList;

    public TableCustomizationData GetTableCustomization(RoomGameType gameType)
    {
        var tableCustomizationData = TableCustomizationList.Find(data => data.GameType == gameType);
        
        if (tableCustomizationData != null)
            return tableCustomizationData.TableCustomizationData;

        Debug.LogError("Not Find TableCustomizationData in TableCustomizationScriptableObject");
        return null;
    }
}

[Serializable]
public class TableCustomizationListElement
{
    public RoomGameType GameType;
    public TableCustomizationData TableCustomizationData;
}
