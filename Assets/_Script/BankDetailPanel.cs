using System.Collections;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class BankDetailPanel : MonoBehaviour
{
    #region PUBLIC_VARIABLES
    public Text txtName;
    public Text txtPhoneNum;
    public Text txtAccountNum;
    public Text txtMsg;

    public TMP_InputField inputTitle;
    public TMP_InputField inputDescription;
    public TMP_InputField inputAccNo;

    public GameObject pnlApprove;

    [Header("Transform")]
    public Transform transformPopup;
    #endregion

    #region UNITY_CALLBACKS
    // Start is called before the first frame update
    void Start()
    {

    }

    void OnEnable()
    {
        txtMsg.text = "";
        UIManager.Instance.SocketGameManager.PlayerAccountInfo((socket, packet, args) =>
        {

            Debug.Log("GetPlayerAccountInfo : " + packet.ToString());

            UIManager.Instance.HideLoader();

            JSONArray arr = new JSONArray(packet.ToString());
            string Source;
            Source = arr.getString(arr.length() - 1);
            var resp1 = Source;
            PokerEventResponse<PlayerAccountInfo> resp = JsonUtility.FromJson<PokerEventResponse<PlayerAccountInfo>>(resp1);

            if (resp.status.Equals(Constants.PokerAPI.KeyStatusSuccess))
            {
                Debug.Log("mobile => " + resp.result.mobile);
                txtName.text = resp.result.username.ToString();
                txtPhoneNum.text = resp.result.mobile.ToString();
                txtAccountNum.text = resp.result.accountNumber.ToString();
            }
            else
            {
                UIManager.Instance.DisplayMessagePanel(resp.message);
            }
        });

        pnlApprove.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {

    }
    #endregion
    public void OnRequestToChangeTap()
    {
        txtMsg.text = "";

        pnlApprove.SetActive(true);
        inputAccNo.text = "";
        inputDescription.text = "";
        inputTitle.text = "";
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
        if (IsDetailValid())
        {
            if (UIManager.Instance.SocketGameManager.HasInternetConnection())
            {
                string title = inputTitle.text;
                string description = inputDescription.text;
                string accno = inputAccNo.text;
                UIManager.Instance.DisplayLoader("");
                UIManager.Instance.SocketGameManager.UpdateAccNo(title, description, accno, (socket, packet, args) =>
                {
                    Debug.Log("UpdateAccountNumber = " + packet.ToString());
                    UIManager.Instance.HideLoader();
                    JSONArray arr = new JSONArray(packet.ToString());
                    string Source;
                    Source = arr.getString(arr.length() - 1);
                    var resp = Source;

                    PokerEventResponse updateResp = JsonUtility.FromJson<PokerEventResponse>(resp);
                    if (updateResp.status.Equals(Constants.PokerAPI.KeyStatusSuccess))
                    {
                        this.Close();
                        UIManager.Instance.assetOfGame.SavedLoginData.accountNumber = inputAccNo.text;
                        UIManager.Instance.LobbyScreeen.ProfileScreen.PanelMyAccount.SelectedOptionButtonTap(0);

                        UIManager.Instance.DisplayMessagePanel(updateResp.message, null);
                    }
                    else
                    {
                        UIManager.Instance.DisplayMessagePanel(updateResp.message, null);
                    }

                });
            }
        }
    }

    private bool IsDetailValid()
    {
        string accNo = inputAccNo.text;

        if (string.IsNullOrEmpty(accNo))
        {
            txtMsg.text = "ID is Empty.";
            //txtMsg.text = "ID is Empty.";
            StartCoroutine(textempti());
            return false;
        }
        if (accNo.Length <= 8)
        {
            Debug.Log("pls check");
            txtMsg.text = "Min size is 9 and Max size is 18";

            //    txtMsg.text = "Min size is 9 and Max size is 18";
            return false;
        }
        if (accNo.Length >= 18)
        {
            Debug.Log("pls check");
            txtMsg.text = "Min size is 9 and Max size is 18";

            //txtMsg.text = "Min size is 9 and Max size is 18";
            return false;
        }

        return true;

    }

    IEnumerator textempti()
    {
        yield return new WaitForSeconds(10.3f);
        txtMsg.text = "";
    }

    /*  private bool IsIDValid()
      {
          string id = inputAccNo.text;
          Regex regex = new Regex(@"[a-zA-Z0-9]\d{2}[a-zA-Z0-9](-\d{3}){2}[A-Za-z0-9]$");
          Match match = regex.Match(id);
          Debug.Log("invalid");
          if (!match.Success)
          {
          Debug.Log("invalid---");
            if (id.Length == 5)
              {
                  Debug.Log("invalid+++++");

                  return false;
              }
          }
          return true;
      }*/
}
