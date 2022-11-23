using System;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "room_preset", menuName = "ScriptableObjects/Room Reference/Preset")]
public class RoomPresetScriptableObject : ScriptableObject
{
    [SerializeField] private List<TableCustomizationListElement> TableCustomizationList;

    public TableCustomizationData GetTableCustomization(RoomGameType gameType)
    {
        var tableCustomizationData = TableCustomizationList.Find(data => data.GameType == gameType);
        
        if (tableCustomizationData != null)
            return tableCustomizationData.TableCustomizationData;

        Debug.LogError($"Not Find GameType: {gameType.ToString()} in Room Preset Scriptable Object");
        return null;
    }
}

[Serializable]
public class TableCustomizationListElement
{
    public RoomGameType GameType;
    public TableCustomizationData TableCustomizationData;
}
