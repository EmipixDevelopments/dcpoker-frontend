using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HomePageTournamentTable : MonoBehaviour
{
    [SerializeField] private HomePageTournamentElement _elemantPrefab;

    private HomePageTournamentElementData[] _elements;
    private int _maxElements = 5;
    private Action _onClickButton;

    public void Init(HomePageTournamentElementData[] elements, Action OnClickButton)
    {
        _elements = elements;
        _onClickButton = OnClickButton;
        ClearElements();
        AddElements(_elements);
    }

    private void AddElements(HomePageTournamentElementData[] elements)
    {
        int allowedAmount = 0;
        if (elements.Length > _maxElements)
        {
            allowedAmount = _maxElements;
        }
        else
        {
            allowedAmount = elements.Length;
        }

        for (int i = 0; i < allowedAmount; i++)
        {
            HomePageTournamentElement element = Instantiate(_elemantPrefab, transform);
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
