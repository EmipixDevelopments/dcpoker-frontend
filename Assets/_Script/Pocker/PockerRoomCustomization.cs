
using System;
using UnityEngine;
using UnityEngine.UI;

public class PockerRoomCustomization : MonoBehaviour
{
    [SerializeField] private RoomBackgroundReferenceScriptableObject _roomBackgroundReferenceScriptableObject;
    [SerializeField] private RoomTableReferenceScriptableObject _roomTableReferenceScriptableObject;
    
    [SerializeField] private RoomPresetScriptableObject _roomPresetScriptableObject;

    [SerializeField] private Image _backgroundImage;
    [SerializeField] private Image _tableImage;
    
    private SaveComponent _saveComponent;
    private TableCustomizationData _currentTableCustomizationData;
    private RoomGameType _currentGameType;

    public void Init()
    {
        _saveComponent = new SaveComponent(); // move from here
        _saveComponent.Init(_roomPresetScriptableObject);
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

    public void UpdateRoom()
    {
        UpdateBackground(_currentTableCustomizationData);
        UpdateTable(_currentTableCustomizationData);
    }
    
    public void UpdateBackground(TableCustomizationData tableCustomizationData)
    {
        var index = (int) tableCustomizationData.RoomColor;
        var colorCount = _roomBackgroundReferenceScriptableObject.Colors.Count;
        
        if (index > colorCount - 1)
        {
            var backgroundImageIndex = index - colorCount;

            _backgroundImage.color = Color.white;
            _backgroundImage.sprite = _roomBackgroundReferenceScriptableObject.BackgroundsImages[backgroundImageIndex];
        }
        else
        {
            _backgroundImage.sprite = null;
            _backgroundImage.color = _roomBackgroundReferenceScriptableObject.Colors[index];
        }
    }

    public void UpdateTable(TableCustomizationData tableCustomizationData)
    {
        var index = (int) tableCustomizationData.TableColor;
        _tableImage.sprite = _roomTableReferenceScriptableObject.TableImages[index];
    }
}
