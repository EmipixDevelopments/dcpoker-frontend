using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class ChangePasswordPanel : MonoBehaviour
{

    #region PUBLIC_VARIABLES
    [Header("Text")]
    public TMP_InputField CurrentPassword;
    public TMP_InputField NewPassword;
    public TMP_InputField ConfirmPassword;
    public TextMeshProUGUI txtMessage;

    [Header("Transform")]
    public Transform transformPopup;
    #endregion

    #region PRIVATE_VARIABLES
    #endregion

    #region UNITY_CALLBACKS
    void OnEnable()
    {
        CurrentPassword.text = "";
        NewPassword.text = "";
        ConfirmPassword.text = "";
        txtMessage.text = "";
    }
    #endregion

    #region DELEGATE_CALLBACKS
    #endregion

    #region PUBLIC_METHODS
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

    public void UpdatePasswordBtnTap()
    {
        UIManager.Instance.SoundManager.OnButtonClick();
        if (IsLoginDetailValid())
        {
            string pass = CurrentPassword.text;
            string npass = NewPassword.text;
            string cpass = ConfirmPassword.text;
            UIManager.Instance.SocketGameManager.GetplayerChangePassword(npass, cpass, pass, (socket, packet, args) =>
            {

                Debug.Log("GetplayerChangePassword  : " + packet.ToString());

                UIManager.Instance.HideLoader();

                //			JSONArray arr = new JSONArray(packet.ToString ());
                //
                //			var resp1 = arr.getString(0);
                JSONArray arr = new JSONArray(packet.ToString());
                string Source;
                Source = arr.getString(arr.length() - 1);
                var resp1 = Source;

                PokerEventResponse resp = JsonUtility.FromJson<PokerEventResponse>(resp1);


                if (resp.status.Equals(Constants.PokerAPI.KeyStatusSuccess))
                {
                    UIManager.Instance.DisplayMessagePanel(resp.message);
                    UIManager.Instance.assetOfGame.SavedLoginData.password = cpass;
                    //					UIManager.Instance.DisplayMessagePanel ("Password Changed SuccessFully!");
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
    #endregion

    #region GETTER_SETTER
    private bool IsLoginDetailValid()
    {
        string pass = CurrentPassword.text;
        string npass = NewPassword.text;
        string cpass = ConfirmPassword.text;

        if (string.IsNullOrEmpty(pass))
        {
            txtMessage.text = Constants.Messages.Register.PasswordEmpty;
            return false;
        }
        else if (string.IsNullOrEmpty(npass))
        {
            txtMessage.text = Constants.Messages.Register.NewPasswordEmpty;

            return false;
        }
        else if (string.IsNullOrEmpty(cpass))
        {
            txtMessage.text = Constants.Messages.Register.ConfirmPasswordEmpty;
            return false;
        }
        else if (npass.Length < Constants.Messages.Login.PasswordLength)
        {
            txtMessage.text = Constants.Messages.Register.MinPasswordLength;
            return false;
        }
        else if (cpass.Length < Constants.Messages.Login.PasswordLength)
        {
            txtMessage.text = Constants.Messages.Register.MinPasswordLength;
            return false;
        }

        return true;
    }

    #endregion
}
