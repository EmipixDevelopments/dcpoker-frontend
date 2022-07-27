using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using System;

public class PanelChangeMyAccountPopup : MonoBehaviour
{
    [SerializeField] private TMP_InputField _newNameInputField;
    [SerializeField] private GameObject _nameExist; 
    [SerializeField] private Button _changeButton;

    string forbiddenName = "";

    private void OnEnable()
    {
        forbiddenName = UIManager.Instance.assetOfGame.SavedLoginData.Username;
    }

    void Update()
    {
        if (NameIsCorrect() && !NameExist())
        {
            _changeButton.interactable = true;
        }
        else
        {
            _changeButton.interactable = false;
        }
    }

    private bool NameExist()
    {
        bool answer = false;
        if (_newNameInputField.text == forbiddenName)
        {
            answer = answer | true;
        }
        if (_newNameInputField.text == UIManager.Instance.assetOfGame.SavedLoginData.Username)
        {
            answer = answer | true;
        }
        return answer;
    }

    private bool NameIsCorrect()
    {
        bool answer = true;
        if (_newNameInputField.text.Length < 4)
        {
            answer = answer & false;
        }
        return answer;
    }

    private void IfNameExist() 
    {
        _nameExist.SetActive(true);
        _newNameInputField.textComponent.color = Color.red;
    }
    private void IfNameNotExist() 
    {
        _nameExist.SetActive(false);
        _newNameInputField.textComponent.color = Color.white;
    }


    public void IfInlutFieldValueChanged() 
    {
        if (NameExist())
        {
            IfNameExist();
        }
        else
        {
            IfNameNotExist();
        }
    }

    public void OnClickChangeButton() 
    {
        string username = _newNameInputField.text;

        UIManager.Instance.SocketGameManager.ChangeUserName(username, (socket, packet, args) =>
        {

            Debug.Log("ChangesUsername response : " + packet.ToString());

            UIManager.Instance.HideLoader();
            PokerEventResponse resp = JsonUtility.FromJson<PokerEventResponse>(Utility.Instance.GetPacketString(packet));

            if (resp.status.Equals(Constants.PokerAPI.KeyStatusSuccess))
            {
                UIManager.Instance.DisplayMessagePanel(resp.message);
                UIManager.Instance.LobbyScreeen.Username = username;
                UIManager.Instance.LobbyScreeen.ProfileScreen.PanelMyAccount.ProfilePanel.Username = username;

                UIManager.Instance.assetOfGame.SavedLoginData.Username = username;
                SaveLoad.SaveGame();
                OnClickCancelButton();
            }
            else if (resp.message == "Username already exists")
            {
                IfNameExist();
            }
            else
            {
                UIManager.Instance.DisplayMessagePanel(resp.message);
            }
        });
    }

    public void OnClickCancelButton() 
    {
        gameObject.SetActive(false);
    }
}
