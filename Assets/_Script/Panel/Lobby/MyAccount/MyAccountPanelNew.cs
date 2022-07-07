using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MyAccountPanelNew : MonoBehaviour
{
    [SerializeField] private GameObject _panelProfile;
    [SerializeField] private GameObject _panelMyBonuses;

    // --- test methods ---//
    public void OpenPanelProfile() 
    {
        CloseAll();
        _panelProfile.SetActive(true);
    }

    public void OpenPanelMyBonuses() 
    {
        CloseAll();
        _panelMyBonuses.SetActive(true);
    }

    public void CloseAll() 
    {
        _panelProfile.SetActive(false);
        _panelMyBonuses.SetActive(false);
    }
    //----------------------
}
