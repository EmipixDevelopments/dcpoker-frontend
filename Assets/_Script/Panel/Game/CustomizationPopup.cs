
using System;
using UnityEngine;
using UnityEngine.UI;

public class CustomizationPopup : MonoBehaviour
{
    [SerializeField] private Button[] _roomColorButtons;
    [SerializeField] private Button[] _tableColorButtons;
    [SerializeField] private Button[] _cardsColorButtons;

    [SerializeField] private GameObject _selectRoomGameObject;
    [SerializeField] private GameObject _selectTableGameObject;
    [SerializeField] private GameObject _selectCardsGameObject;

    [SerializeField] private Toggle _toggle;

    [SerializeField] private Button _closeButton;
    [SerializeField] private Button _saveButton;

    private PockerRoomCustomization _pockerRoomCustomization;
    private TableCustomizationData _tableCustomizationData;
    private TableCustomizationData _currentCustomizationData;

    public void Init(PockerRoomCustomization pockerRoomCustomization)
    {
        _pockerRoomCustomization = pockerRoomCustomization;
        _currentCustomizationData = new TableCustomizationData();
    }

    private void Start()
    {
        _closeButton.onClick.AddListener(OnCloseButton);
        _saveButton.onClick.AddListener(OnSaveButton);
        
        _toggle.onValueChanged.AddListener(OnToggleChange);
        
        InitButtons();
    }

    private void OnEnable()
    {
        _tableCustomizationData = _pockerRoomCustomization.GetCurrentCustomizationData();
        _currentCustomizationData.Copy(_tableCustomizationData);
        UpdateSelect();
    }

    private void UpdateSelect()
    {
        _selectRoomGameObject.gameObject.transform.position = 
            _roomColorButtons[(int)_tableCustomizationData.RoomColor].gameObject.transform.position;
        
        _selectTableGameObject.gameObject.transform.position = 
            _tableColorButtons[(int)_tableCustomizationData.TableColor].gameObject.transform.position;
        
        _selectCardsGameObject.gameObject.transform.position = 
            _cardsColorButtons[(int)_tableCustomizationData.CardsColor].gameObject.transform.position;
    }

    private void OnCloseButton()
    {
        _pockerRoomCustomization.UpdateRoom();
        gameObject.SetActive(false);
    }

    private void InitButtons()
    {
        InitButtonsArray(_selectRoomGameObject, _roomColorButtons, OnSelectRoomColor);
        InitButtonsArray(_selectTableGameObject, _tableColorButtons, OnSelectTableColor);
        InitButtonsArray(_selectCardsGameObject, _cardsColorButtons, OnSelectCardsColor);
    }

    private void InitButtonsArray(GameObject select, Button[] buttons, Action<int> onSelectAction)
    {
        for(var i = 0; i < buttons.Length; i++)
        {
            var currentButton = buttons[i];
            var index = i;
            
            currentButton.onClick.AddListener(() =>
            {
                select.gameObject.transform.position = currentButton.gameObject.transform.position;
                onSelectAction.Invoke(index);
            });
        }
    }
    
    private void OnSelectRoomColor(int index)
    {
        _currentCustomizationData.RoomColor = (RoomColor)index;
        _pockerRoomCustomization.UpdateRoom(_currentCustomizationData);
    }

    private void OnSelectTableColor(int index)
    {
        
    }

    private void OnSelectCardsColor(int index)
    {
        
    }

    private void OnToggleChange(bool value)
    {
        
    }
    
    private void OnSaveButton()
    {
        _pockerRoomCustomization.Save(_currentCustomizationData);
        gameObject.SetActive(false);
    }

    public void OpenClose()
    {
        if(gameObject.activeSelf)
            OnCloseButton();
        else
            gameObject.SetActive(true);
    }
}
