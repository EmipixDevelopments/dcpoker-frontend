using System.Collections.Generic;
using UnityEngine;
public class SaveComponent
{
    private RoomPresetScriptableObject _roomPresetScriptableObject;
    private Dictionary<RoomGameType, TableCustomizationData> _tableCustomizationsData;

    public SaveComponent()
    {
        _tableCustomizationsData = new Dictionary<RoomGameType, TableCustomizationData>();
    }

    public void Init(RoomPresetScriptableObject roomPresetScriptableObject) //move to container or context
    {
        _roomPresetScriptableObject = roomPresetScriptableObject;
    }

    public TableCustomizationData GetTableCustomization(RoomGameType gameType)
    {
        if (_tableCustomizationsData.ContainsKey(gameType))
        {
            return _tableCustomizationsData[gameType];
        }
        
        var tableCustomizationsDataJson =
            PlayerPrefs.GetString($"table_customization_{gameType.ToString()}" , GetDefaultTableCustomizations(gameType));

        var tableCustomizationsData = JsonUtility.FromJson<TableCustomizationData>(tableCustomizationsDataJson);
        _tableCustomizationsData.Add(gameType, tableCustomizationsData);
        
        return tableCustomizationsData;
    }

    public void SaveTableCustomizationData(RoomGameType gameType)
    {
        if(!_tableCustomizationsData.ContainsKey(gameType))
            return;

        var tableCustomizationsDataJson = JsonUtility.ToJson(_tableCustomizationsData[gameType]);
        PlayerPrefs.SetString($"table_customization_{gameType.ToString()}", tableCustomizationsDataJson);
    }
    
    private string GetDefaultTableCustomizations(RoomGameType gameType)
    {
        var tableCustomizationData = _roomPresetScriptableObject.GetTableCustomization(gameType);
        return JsonUtility.ToJson(tableCustomizationData);
    }
}
