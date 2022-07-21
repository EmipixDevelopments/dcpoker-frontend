using System.Collections;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using System.Text.RegularExpressions;

public class forgetPasswordPanel : MonoBehaviour
{
     #region PUBLIC_VARIABLES
    public TMP_InputField ValidEmail;
    //	public InputField ValidEmail;
    public Text ErrorText;
    #endregion

    #region PUBLIC_METHODS
    public void closeButtonTap()
    {
        UIManager.Instance.SoundManager.OnButtonClick();
        this.Close();
    }
    public void OnSubmitButtonTap()
    {
        if (UIManager.Instance.SocketGameManager.HasInternetConnection())
        {
            UIManager.Instance.SoundManager.OnButtonClick();
            if (IsEmailValid())
            {
                ErrorText.text = "";
                string Email = ValidEmail.text;
                UIManager.Instance.SocketGameManager.PlayerNewPassword(Email, (socket, packet, args) =>
                {

                    Debug.Log("GetplayerForgotPassword  : " + packet.ToString());

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
                        UIManager.Instance.assetOfGame.SavedLoginData.password = "";
                        ValidEmail.text = "";
                    }
                    else
                    {
                        UIManager.Instance.DisplayMessagePanel(resp.message);
                    }
                });

            }
            else
            {
                ErrorText.text = Constants.Messages.Register.EmailInvalid;
            }
        }
    }

    #endregion

    #region COROUTINES
    //	IEnumerator
    IEnumerator textempti()
    {

        yield return new WaitForSeconds(1.3f);
        ErrorText.text = "";
    }
    #endregion


    #region GETTER_SETTER


    private bool IsEmailValid()
    {
        string email = ValidEmail.text;
        Regex regex = new Regex(@"^([\w\.\-]+)@([\w\-]+)((\.(\w){2,3})+)$");
        Match match = regex.Match(email);

        if (!match.Success)
        {
            StartCoroutine(textempti());
            return false;
        }
        return true;
    }

    #endregion

}
