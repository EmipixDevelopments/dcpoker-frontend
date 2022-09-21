
using System;
using System.Collections.Generic;
using UnityEngine;
using Object = UnityEngine.Object;

public class TableContainer<T> where T : MonoBehaviour
{
    private List<T> _list;
    
    private Transform _container;
    private T _element;

    private Action<T> _initAction;

    private bool _isFirstOrderInstance;

    public TableContainer(Transform container, T element, Action<T> initAction, bool isFirstOrderInstance = false)
    {
        _list = new List<T>();
        
        _container = container;
        _element = element;

        _initAction = initAction;

        _isFirstOrderInstance = isFirstOrderInstance;
    }

    public T GetElement(int index)
    {
        while (_list.Count <= index)
            CreateElement();

        var result = _list[index];
        result.gameObject.SetActive(true);
        
        return result;
    }

    public void HideFromIndex(int index)
    {
        if(_list.Count < index)
            return;
        
        for (var i = index; i < _list.Count; i++)
        {
           _list[i].gameObject.SetActive(false); 
        }
    }

    public void HideWithCheckFromIndex(int index)
    {
        if(_list.Count < index)
            return;
        
        for (var i = index; i < _list.Count; i++)
        {
            if(_list[i].gameObject.activeSelf == false)
                break;
            
            _list[i].gameObject.SetActive(false); 
        }
    }

    private void CreateElement()
    {
        var element = Object.Instantiate(_element, _container);
        
        if (_isFirstOrderInstance)
            element.transform.SetAsFirstSibling();

        _initAction?.Invoke(element);
        
        _list.Add(element);
    }
}

