using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;

public class OtherUserNamePanel : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _name1;
    [SerializeField] private TextMeshProUGUI _name2;
    [SerializeField] private TextMeshProUGUI _name3;

    public Action<string> OnSelectName;

    public void Init(string[] names) 
    {
        _name1.text = names[0];
        _name2.text = names[1];
        _name3.text = names[2];
    }

    public void OnButtonName1Click()
    {
        OnSelectName?.Invoke(_name1.text);
        CloseWindow();
    }
    public void OnButtonName2Click()
    {
        OnSelectName?.Invoke(_name2.text);
        CloseWindow();
    }
    public void OnButtonName3Click()
    {
        OnSelectName?.Invoke(_name3.text);
        CloseWindow();
    }

    private void CloseWindow() 
    {
        gameObject.SetActive(false);
    }
}
