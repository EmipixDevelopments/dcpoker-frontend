using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PanelForgotPasswordNew : MonoBehaviour
{
    #region PUBLIC_VARIABLES

    public GameObject panelUsername;
    public GameObject panelRecoveryVerify;
    public GameObject panelRecoveryVerifySuccess;
    public GameObject panelRecoveryVerifyFail;

    #endregion

    #region PRIVATE_VARIABLES

    #endregion

    private void VerifyPhrase()
    {
//        if (UIManager.Instance.SocketGameManager.HasInternetConnection())
//        {
//            UIManager.Instance.SoundManager.OnButtonClick();
//            UIManager.Instance.DisplayLoader("");
//
//            UIManager.Instance.SocketGameManager.PlayerRecoveryPhraseVerification(_username, _password, _playerEnteredPhrase, (socket, packet, args) =>
//            {
//                Debug.Log("Verify Phrase  => " + packet.ToString());
//                UIManager.Instance.HideLoader();
//                JSONArray arr = new JSONArray(packet.ToString());
//                string Source;
//                Source = arr.getString(arr.length() - 1);
//                var resp = Source;
//                PokerEventResponse registrationResp = JsonUtility.FromJson<PokerEventResponse>(resp);
//
//                if (registrationResp.status.Equals(Constants.PokerAPI.KeyStatusSuccess))
//                {
//                    panelRecoveryVerifySuccess.SetActive(true);
//                    panelRecoveryVerify.SetActive(false);
//                }
//                else if (registrationResp.message == "Username already taken.")
//                {
//                    panelUsername.SetActive(true);
//                    panelPassword.SetActive(false);
//                }
//                else
//                {
//                    UIManager.Instance.DisplayMessagePanel(registrationResp.message, null);
//                }
//            });
//        }
    }

    #region UI_CALLBACKS

    public void On_Button_Click_Username_Next()
    {
//        if (IsUsernameValid())
//        {
//            _username = inputFieldUsername.text;
//            panelUsername.SetActive(false);
//            panelPassword.SetActive(true);
//        }
    }

    public void On_Button_Click_RecoveryVerify_Token(int index)
    {
//        if (string.IsNullOrEmpty(_playerEnteredPhrase))
//            _playerEnteredPhrase = _listPhrasePartition[index];
//        else
//            _playerEnteredPhrase += " " + _listPhrasePartition[index];
//
//        textPhraseVerificationText.text = _playerEnteredPhrase;
    }

    public void On_Button_Click_RecoveryVerify_Next()
    {
        VerifyPhrase();
    }

    public void On_Button_Click_RecoveryVerify_Success()
    {
    }

    public void On_Button_Click_RecoveryVerify_Fail()
    {
    }

    #endregion
}