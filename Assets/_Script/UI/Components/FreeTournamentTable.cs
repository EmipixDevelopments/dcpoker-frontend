using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FreeTournamentTable : MonoBehaviour
{
    [SerializeField] private FreeTournamentTableElement _elemantPrefab;

    private FreeTournamentTableElementData[] _elements;
    private int _maxElements = 5;
    private Action _onClickButton;

    public void Init(FreeTournamentTableElementData[] elements, Action OnClickButton)
    {
        _elements = elements;
        _onClickButton = OnClickButton;
        ClearElements();
        AddElements(_elements);
    }

    private void AddElements(FreeTournamentTableElementData[] elements)
    {
        for (int i = 0; i < _maxElements; i++)
        {
            FreeTournamentTableElement element = Instantiate(_elemantPrefab, transform);
            element.Init(elements[i], _onClickButton);
        }
    }

    private void ClearElements()
    {
        for (int i = 0; i < transform.childCount; i++)
        {
            Destroy(transform.GetChild(i));
        }
    }
}
