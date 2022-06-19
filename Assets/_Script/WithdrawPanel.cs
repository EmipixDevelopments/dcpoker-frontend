using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class WithdrawPanel : MonoBehaviour
{
    public TMP_InputField inputAmount;
    public Text txtError;
    [Header("Transform")]
    public Transform transformPopup;
    // Start is called before the first frame update
    void OnEnable()
    {
        Reset();
        txtError.text = "";
    }

    // Update is called once per frame
    void Update()
    {

    }
    private void Reset()
    {
        inputAmount.text = "";
    }
    public void KeyboardOpen(float positionY)
    {
#if UNITY_EDITOR || UNITY_IOS || UNITY_ANDROID
        transformPopup.localPosition = new Vector3(0, positionY, 0);
#endif
    }

    public void KeyboardClose()
    {
#if UNITY_EDITOR || UNITY_IOS || UNITY_ANDROID
        transformPopup.localPosition = new Vector3(0, 0, 0);
#endif
    }
    public void SubmitButtonTap()
    {
        if (isValid())
        {
            if (UIManager.Instance.SocketGameManager.HasInternetConnection())
            {
                string amount = inputAmount.text;

                if (amount.All(char.IsDigit))
                {
                    UIManager.Instance.DisplayLoader("");
                    UIManager.Instance.SocketGameManager.UploadWithdraw(amount, (socket, packet, args) =>
                {
                    Debug.Log("UploadWithdraw = " + packet.ToString());
                    UIManager.Instance.HideLoader();
                    JSONArray arr = new JSONArray(packet.ToString());
                    string Source;
                    Source = arr.getString(arr.length() - 1);
                    var resp = Source;

                    PokerEventResponse updateResp = JsonUtility.FromJson<PokerEventResponse>(resp);
                    if (updateResp.status.Equals(Constants.PokerAPI.KeyStatusSuccess))
                    {
                        UIManager.Instance.DisplayMessagePanel(updateResp.message, null);
                        Reset();
                        StartCoroutine("ClosePanel");
                    }
                    else
                    {
                        UIManager.Instance.DisplayMessagePanel(updateResp.message, null);
                    }
                });
                }
                else
                {
                    txtError.text = "Enter Only Digits";
                    StartCoroutine(textempti());
                }
            }
        }
    }
    IEnumerator ClosePanel()
    {
        yield return new WaitForSeconds(0.5f);
        this.Close();
        UIManager.Instance.LobbyScreeen.ProfileScreen.PanelMyAccount.SelectedOptionButtonTap(0);
    }

    private bool isValid()
    {
        txtError.text = "";

        string amount = inputAmount.text;

        if (string.IsNullOrEmpty(amount))
        {
            // txtError.text = "Amount is Empty.";
            txtError.text = "Amount is Empty.";
            StartCoroutine(textempti());
            return false;
        }
        return true;
    }
    IEnumerator textempti()
    {
        yield return new WaitForSeconds(1f);
        txtError.text = "";
    }
}
