﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;

public class PanelProfileNew : MonoBehaviour
{
    [SerializeField] private Image _avatarImage;
    [SerializeField] private TextMeshProUGUI _userName;
    [SerializeField] private TextMeshProUGUI _chipsValue;
    [SerializeField] private TextMeshProUGUI _cashValue;
    [SerializeField] private TextMeshProUGUI _phoneNumber;
    [SerializeField] private Toggle _chipsToggle;
    [SerializeField] private Toggle _cashToggle;

    private int _avatarImageIndex = -1;


    void OnEnable()
    {
        if (UIManager.Instance)
        {
            CallProfileEvent();
        }
    }


    private void FixedUpdate()
    {
        // autoupdate
        UpdateFields();
    }

    // not used
    //private void ChangePassword() 
    //{
    //        UIManager.Instance.SoundManager.OnButtonClick();
    //        string newPassword = "112233";
    //        UIManager.Instance.SocketGameManager.PlayerNewPassword(newPassword, (socket, packet, args) =>
    //        {
    //            Debug.Log("GetplayerForgotPassword  : " + packet.ToString());
    //
    //            UIManager.Instance.HideLoader();
    //            JSONArray arr = new JSONArray(packet.ToString());
    //            string Source;
    //            Source = arr.getString(arr.length() - 1);
    //            var resp1 = Source;
    //
    //            PokerEventResponse resp = JsonUtility.FromJson<PokerEventResponse>(resp1);
    //
    //            if (resp.status.Equals(Constants.PokerAPI.KeyStatusSuccess))
    //            {
    //                UIManager.Instance.DisplayMessagePanel(resp.message);
    //                UIManager.Instance.assetOfGame.SavedLoginData.password = newPassword;
    //            }
    //            else
    //            {
    //                UIManager.Instance.DisplayMessagePanel(resp.message);
    //            }
    //        });
    //}

    public void CallProfileEvent()
    {
        UIManager.Instance.SocketGameManager.GetProfile((socket, packet, args) =>
        {
            Debug.Log("GetProfile  : " + packet.ToString());

            UIManager.Instance.HideLoader();
            JSONArray arr = new JSONArray(packet.ToString());
            string Source;
            Source = arr.getString(arr.length() - 1);
            var resp1 = Source;

            PokerEventResponse<PlayerLoginResponse> resp = JsonUtility.FromJson<PokerEventResponse<PlayerLoginResponse>>(resp1);

            if (resp.status.Equals(Constants.PokerAPI.KeyStatusSuccess))
            {
                UIManager.Instance.assetOfGame.SavedLoginData.SelectedAvatar = resp.result.profilePic;
                UIManager.Instance.assetOfGame.SavedLoginData.chips = resp.result.chips;
                UIManager.Instance.assetOfGame.SavedLoginData.cash = resp.result.cash;
                UIManager.Instance.assetOfGame.SavedLoginData.Username = resp.result.username;
                UIManager.Instance.assetOfGame.SavedLoginData.PlayerId = resp.result.playerId;
                UpdateFields();

                if (resp.result.isCash)
                {
                    _cashToggle.isOn = true;
                }
                else
                {
                    _chipsToggle.isOn = true;
                }
            }
            else
            {
                UIManager.Instance.DisplayMessagePanel(resp.message);
            }
        });
    }

    public void OnClickChipsOrCashs() 
    {
        // send request on change pay Type
        bool isCashe = _cashToggle.isOn;
        Debug.Log($"user select cashe {isCashe}");
    }

    private void UpdateFields()
    {
        _avatarImage.sprite = UIManager.Instance.assetOfGame.profileAvatarList.profileAvatarSprite[UIManager.Instance.assetOfGame.SavedLoginData.SelectedAvatar];
        _chipsValue.text = UIManager.Instance.assetOfGame.SavedLoginData.chips.ToString();
        _cashValue.text = UIManager.Instance.assetOfGame.SavedLoginData.cash.ToString();
        _phoneNumber.text = $"{UIManager.Instance.assetOfGame.SavedLoginData.phoneCode} {UIManager.Instance.assetOfGame.SavedLoginData.phoneNumber}";
        _userName.text = UIManager.Instance.assetOfGame.SavedLoginData.Username;
    }
}
