
using System.Collections.Generic;
using UnityEngine;

public class TableContainer<T> where T : MonoBehaviour
{
    private List<T> _list;
    
    private Transform _container;
    private T _element;

    public TableContainer(Transform container, T element)
    {
        _list = new List<T>();
        
        _container = container;
        _element = element;
    }

    public T GetElement(int index)
    {
        while (_list.Count <= index)
            CreateElement();

        return _list[index];
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
        _list.Add(Object.Instantiate(_element, _container));
    }
}

