using System;
using UnityEngine;

public class HomeOmahaPolo5TableList : MonoBehaviour
{
    [SerializeField] private HomeSmallTableElement _prefab;
    private int _amountElements = 5;

    private void Start()
    {
        Init();
    }

    private void Init()
    {
        for (var i = 0; i < _amountElements; i++)
        {
            Instantiate(_prefab, transform);
        }
    }
    
    public void UpdateList()
    {
        
    }
}
