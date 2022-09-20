
using System;
using UnityEngine;
using UnityEngine.UI;

public class PockerRoomCustomization : MonoBehaviour
{
    [SerializeField] private TableBackgroundReferenceScriptableObject _tableBackgroundReferenceScriptableObject;
    [SerializeField] private TableCustomizationsScriptableObject _tableCustomizationsScriptableObject;

    [SerializeField] private Image _backgroundImage;
    
    private SaveComponent _saveComponent;
    private TableCustomizationData _currentTableCustomizationData;
    private RoomGameType _currentGameType;

    public void Init()
    {
        _saveComponent = new SaveComponent(); // move from here
        _saveComponent.Init(_tableCustomizationsScriptableObject);
    }

    public TableCustomizationData GetCurrentCustomizationData()
        => _currentTableCustomizationData;

    public void SetData(RoomData roomData)
    {
        if(_currentGameType == RoomGameType.None)
        {
            Debug.LogError("Room type must not be none");
            return;
        }
        
        _currentTableCustomizationData = _saveComponent.GetTableCustomization(roomData.GetRoomType());
        _currentGameType = roomData.GetRoomType();
        
        UpdateRoom();
    }

    public void Save(TableCustomizationData tableCustomizationData)
    {
        _currentTableCustomizationData.Copy(tableCustomizationData);
        _saveComponent.SaveTableCustomizationData(_currentGameType);
    }

    public void UpdateRoom() => UpdateRoom(_currentTableCustomizationData);

    public void UpdateRoom(TableCustomizationData tableCustomizationData)
    {
        switch (tableCustomizationData.RoomColor)
        {
            case RoomColor.Blue:
                _backgroundImage.sprite = null;
                _backgroundImage.color = _tableBackgroundReferenceScriptableObject.Colors[0];
                break;
            case RoomColor.DarkBlue:
                _backgroundImage.sprite = null;
                _backgroundImage.color = _tableBackgroundReferenceScriptableObject.Colors[1];
                break;
            case RoomColor.Green:
                _backgroundImage.sprite = null;
                _backgroundImage.color = _tableBackgroundReferenceScriptableObject.Colors[2];
                break;
            case RoomColor.Red:
                _backgroundImage.sprite = null;
                _backgroundImage.color = _tableBackgroundReferenceScriptableObject.Colors[3];
                break;
            case RoomColor.Gold:
                _backgroundImage.sprite = null;
                _backgroundImage.color = _tableBackgroundReferenceScriptableObject.Colors[4];
                break;
            
            case RoomColor.PatternBlue:
                _backgroundImage.sprite = _tableBackgroundReferenceScriptableObject.BackgroundsImages[0];
                _backgroundImage.color = Color.white;
                break;
            case RoomColor.PatternDarkBlue:
                _backgroundImage.sprite = _tableBackgroundReferenceScriptableObject.BackgroundsImages[1];
                _backgroundImage.color = Color.white;
                break;
            case RoomColor.PatternRed:
                _backgroundImage.sprite = _tableBackgroundReferenceScriptableObject.BackgroundsImages[2];
                _backgroundImage.color = Color.white;
                break;
            case RoomColor.PatternGreen:
                _backgroundImage.sprite = _tableBackgroundReferenceScriptableObject.BackgroundsImages[3];
                _backgroundImage.color = Color.white;
                break;
            case RoomColor.PatternGold:
                _backgroundImage.sprite = _tableBackgroundReferenceScriptableObject.BackgroundsImages[4];
                _backgroundImage.color = Color.white;
                break;
        }
    }
}
