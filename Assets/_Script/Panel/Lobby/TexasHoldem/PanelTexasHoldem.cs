using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PanelTexasHoldem : MonoBehaviour
{
    [SerializeField] private PanelTexasHoldemFilterPanel _filtersPanel;
    [SerializeField] private TexasHoldemElement _texasHoldemPrefab;
    [SerializeField] private Transform _content;
    private int _updatePanelAfterSecconds = 8;
    private List<TexasHoldemElement> _tableElements = new List<TexasHoldemElement>();

    private void Start()
    {
    }
    private void OnDestroy()
    {
    }

    private void OnEnable()
    {
        StopAllCoroutines();
        StartCoroutine(UpdateAtTime());
    }

    IEnumerator UpdateAtTime()
    {
        while (true)
        {
            UpdateTable();
            yield return new WaitForSeconds(_updatePanelAfterSecconds);
        }
    }
    private void RemoveAllRow()
    {
        for (int i = 0; i < _content.childCount; i++)
        {
            Destroy(_content.GetChild(i).gameObject);
        }
        _tableElements.Clear();
    }

    private void UpdateTable()
    {
        throw new NotImplementedException();
    }
}
