using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class termsAndConditonspanel : MonoBehaviour
{
    #region PUBLIC_VARIABLES
    public ScrollRect Scrolls;
    public Text txtTerms;
    public Text txtNoData;
    public Button CloseBtn;
    #endregion

    #region PRIVATE_VARIABLES

    #endregion

    #region UNITY_CALLBACKS
    private void OnEnable()
    {
        StartCoroutine(ForceScrollupTNC());
    }
    private void OnDisable()
    {
        txtNoData.Close();
        Scrolls.Open();
        CloseBtn.Close();
        txtTerms.text = "";
    }
    #endregion

    #region DELEGATE_CALLBACKS


    #endregion

    #region PUBLIC_METHODS
    public void SetDataOpen()
    {
        txtNoData.Close();
        CloseBtn.Close();
        UIManager.Instance.SocketGameManager.RulesofPlay((socket, packet, args) =>
        {

            Debug.Log("RulesofPlay  : " + packet.ToString());

            UIManager.Instance.HideLoader();

            //			JSONArray arr = new JSONArray(packet.ToString ());
            //
            //			var resp1 = arr.getString(0);
            JSONArray arr = new JSONArray(packet.ToString());
            string Source;
            Source = arr.getString(arr.length() - 1);
            var resp1 = Source;

            PokerEventResponse<TermsResult> resp = JsonUtility.FromJson<PokerEventResponse<TermsResult>>(resp1);

            if (resp.status.Equals(Constants.PokerAPI.KeyStatusSuccess))
            {
                Scrolls.Open();
                txtTerms.text = "";
                txtTerms.text = resp.result.content;
                this.Open();
            }
            else
            {
                Scrolls.Close();
                txtTerms.text = "";
                CloseBtn.Open();
                txtNoData.Open();
                this.Open();
                //UIManager.Instance.DisplayMessagePanel(resp.message);
            }
        });
    }
    public void CloseButtonTap()
    {
        UIManager.Instance.SoundManager.OnButtonClick();
        this.Close();
    }

    #endregion

    #region PRIVATE_METHODS


    #endregion

    #region COROUTINES
    IEnumerator ForceScrollupTNC()
    {
        // Wait for end of frame AND force update all canvases before setting to bottom.
        yield return new WaitForEndOfFrame();
        if (Scrolls.isActiveAndEnabled)
        {
            Canvas.ForceUpdateCanvases();
            Scrolls.verticalScrollbar.value = 1;
            Canvas.ForceUpdateCanvases();
        }
    }
    #endregion


    #region GETTER_SETTER

    #endregion

}