using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MyAccountPanelNew : MonoBehaviour
{
    [SerializeField] private MyAccountMenu _myAccountMenu;

    private void OnEnable()
    {
        _myAccountMenu.ProfileToggle.isOn = true;
    }
}
