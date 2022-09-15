using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PopupConfirmTournament : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _text;
    [SerializeField] private Button _confirmButton;
    [SerializeField] private Button _cancelButton;
    private PopupConfirmTournamentData _data;

    private void Start()
    {
        _confirmButton.onClick.AddListener(OnConfirmButton);
        _cancelButton.onClick.AddListener(OnCloseButton);
    }

    private void OnDestroy()
    {
        _confirmButton.onClick.RemoveListener(OnConfirmButton);
        _cancelButton.onClick.RemoveListener(OnCloseButton);
    }

    private void OnCloseButton()
    {
        gameObject.SetActive(false);    
    }
    
    private void OnConfirmButton()
    {
        _data?.ConfirmAction?.Invoke();
        OnCloseButton();
    }

    public void OpenPopup(PopupConfirmTournamentData data)
    {
        _data = data;
        if(_data == null)
            return;

        if (!gameObject.activeSelf)
        {
            gameObject.SetActive(true);
        }

        UpdateData();
    }

    private void UpdateData()
    {
        var totalSum = _data.FirstValue + _data.SecondValue + _data.ThirdValue;
        _text.text = string.Format(_text.text, _data.FirstValue, _data.SecondValue, _data.ThirdValue, totalSum);
    }
}

public class PopupConfirmTournamentData
{
    public Action ConfirmAction;
    public float FirstValue;
    public float SecondValue;
    public float ThirdValue;
}
