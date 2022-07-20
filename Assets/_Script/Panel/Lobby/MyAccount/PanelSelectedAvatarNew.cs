﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PanelSelectedAvatarNew : MonoBehaviour
{
    [SerializeField] private List<Toggle> _avatarToggles = new List<Toggle>();

    private int _avatarId = 0;

    private void OnEnable()
    {
        Init();
    }

    void Init() 
    {
        if (UIManager.Instance)
        {
            _avatarId = UIManager.Instance.assetOfGame.SavedLoginData.SelectedAvatar;
        }
        _avatarToggles[_avatarId].isOn = true;
    }


    public void OnClickCloseButton() 
    {
        gameObject.SetActive(false);
    }

    public void OnClickAvatar(int index) 
    {
        _avatarId = index;
    }

    public void OnClickSelectButton() 
    {
        UIManager.Instance.assetOfGame.SavedLoginData.SelectedAvatar = _avatarId;
        OnClickCloseButton();
    }
}
