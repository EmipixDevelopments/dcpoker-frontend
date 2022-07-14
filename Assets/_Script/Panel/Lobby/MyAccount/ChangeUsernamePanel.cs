using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class ChangeUsernamePanel : MonoBehaviour
{

    #region PUBLIC_VARIABLES
    public InputField inputNewUsername;
    //	public InputField inputNewUsername;
    public TextMeshProUGUI txtMessage;

    [Header("Transform")]
    public Transform transformPopup;
    #endregion

    #region PRIVATE_VARIABLES

    #endregion

    #region UNITY_CALLBACKS
    // Use this for initialization
    void OnEnable()
    {
        inputNewUsername.text = "";
        txtMessage.text = "";
    }
    #endregion

    #region DELEGATE_CALLBACKS


    #endregion

    #region PUBLIC_METHODS

    public void LowerCapsFunction()
    {
        inputNewUsername.text = inputNewUsername.text.ToLower();
    }

    public void KeyboardOpen(float positionY)
    {
#if UNITY_EDITOR || UNITY_IOS || UNITY_ANDROID
        transformPopup.localPosition = new Vector3(96, positionY, 0);
#endif
    }

    public void KeyboardClose()
    {
#if UNITY_EDITOR || UNITY_IOS || UNITY_ANDROID
        transformPopup.localPosition = new Vector3(96, 0, 0);
#endif
    }

    public void UpdateUsernameBtnTap()
    {
        UIManager.Instance.SoundManager.OnButtonClick();
        if (Validation())
        {
            string username = inputNewUsername.text;

            if (username == UIManager.Instance.assetOfGame.SavedLoginData.Username)
            {
                ClosePanelBtnTap(false);
                return;
            }

            UIManager.Instance.SocketGameManager.ChangeUsername(username, (socket, packet, args) =>
            {

                Debug.Log("ChangesUsername response : " + packet.ToString());

                UIManager.Instance.HideLoader();
                PokerEventResponse resp = JsonUtility.FromJson<PokerEventResponse>(Utility.Instance.GetPacketString(packet));

                if (resp.status.Equals(Constants.PokerAPI.KeyStatusSuccess))
                {
                    UIManager.Instance.DisplayMessagePanel(resp.message);
                    UIManager.Instance.assetOfGame.SavedLoginData.Username = username;
                    UIManager.Instance.LobbyScreeen.Username = username;
                    UIManager.Instance.LobbyScreeen.ProfileScreen.PanelMyAccount.ProfilePanel.Username = username;

                    UIManager.Instance.assetOfGame.SavedLoginData.Username = username;
                    SaveLoad.SaveGame();

                    ClosePanelBtnTap(false);
                }
                else
                {
                    UIManager.Instance.DisplayMessagePanel(resp.message);
                }
            });

        }
    }

    public void ClosePanelBtnTap(bool IsOPen)
    {
        UIManager.Instance.SoundManager.OnButtonClick();
        if (IsOPen)
        {
            this.Open();
        }
        else
        {
            this.Close();
        }

    }
    #endregion

    #region PRIVATE_METHODS

    #endregion

    #region COROUTINES
    IEnumerator textempti()
    {
        yield return new WaitForSeconds(4.3f);
        txtMessage.text = "";
    }

    #endregion

    #region GETTER_SETTER
    private bool Validation()
    {
        string username = inputNewUsername.text;

        if (string.IsNullOrEmpty(username))
        {
            txtMessage.text = Constants.Messages.Register.UsernameEmpty;
            StopCoroutine(textempti());
            StartCoroutine(textempti());
            return false;
        }
        else if (!IsUsernameValid())
        {
            txtMessage.text = Constants.Messages.Register.UsernameInvalid;
            StopCoroutine(textempti());
            StartCoroutine(textempti());
            return false;
        }

        return true;
    }
    private bool IsUsernameValid()
    {
        string number = inputNewUsername.text;

        if (number.Length < 4)
        {
            return false;
        }
        return true;
    }
    #endregion
}
