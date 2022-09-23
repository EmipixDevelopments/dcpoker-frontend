using System;
using UnityEngine;
using UnityEngine.UI;

public class PlayerPlace : MonoBehaviour
{
    [SerializeField] private Button _openSeatButton;

    public void Init()
    {
        
    }

    public void SetOpenSeat(Action action)
    {
        _openSeatButton.onClick.RemoveAllListeners();
        _openSeatButton.onClick.AddListener(action.Invoke);
        _openSeatButton.gameObject.SetActive(true);
    }

    public void SetActiveOpenSeatButton(bool active)
    {
        _openSeatButton.gameObject.SetActive(active);
    }

    public void Reset()
    {
        
    }
}
