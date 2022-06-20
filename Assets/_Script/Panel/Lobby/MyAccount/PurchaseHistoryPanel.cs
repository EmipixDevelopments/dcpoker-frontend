using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;


public class PurchaseHistoryPanel : MonoBehaviour
{

    #region PUBLIC_VARIABLES
    [Header("Text")]
    public TextMeshProUGUI txtFromTo;

    [Header("Transforms")]
    public Transform HistoryDataParent;

    [Header("ScriptableObjects")]
    public PurchaseDataHistory PurchasehistoryDataObj;
    #endregion

    #region PRIVATE_VARIABLES

    #endregion

    #region UNITY_CALLBACKS
    // Use this for initialization
    void OnEnable()
    {
        UIManager.Instance.SoundManager.OnButtonClick();
        /* if (UIManager.Instance.assetOfGame.SavedLoginData.isCash)
         {
             txtFromTo.Open();
         }
         else
         {
             txtFromTo.Close();
         }
         */
        DestroyData();
        UIManager.Instance.SocketGameManager.GetPurchaseHistory((socket, packet, args) =>
        {

            Debug.Log("GetpurchaseHistory  : " + packet.ToString());
            UIManager.Instance.HideLoader();

            purchaseHistory resp = JsonUtility.FromJson<purchaseHistory>(Utility.Instance.GetPacketString(packet));

            if (resp.status.Equals(Constants.PokerAPI.KeyStatusSuccess))
            {
                for (int i = 0; i < resp.result.Count; i++)
                {
                    PurchaseDataHistory PurchasehistoryDataList = Instantiate(PurchasehistoryDataObj) as PurchaseDataHistory;
                    PurchasehistoryDataList.SetData(resp.result[i], i);
                    PurchasehistoryDataList.transform.SetParent(HistoryDataParent, false);
                }
            }
            else
            {
                UIManager.Instance.DisplayMessagePanel(resp.message);
            }
        });
    }
    void OnDisable()
    {
        DestroyData();
    }
    // Update is called once per frame
    void Update()
    {

    }
    #endregion

    #region DELEGATE_CALLBACKS


    #endregion

    #region PUBLIC_METHODS

    #endregion

    #region PRIVATE_METHODS
    /*void StaticDataDisplay()
	{
		for (int i = 0; i < 10; i++) {

			PurchaseDataHistory PurchasehistoryDataList = Instantiate (PurchasehistoryDataObj) as PurchaseDataHistory;
			PurchasehistoryDataList.SetData ();
			PurchasehistoryDataList.transform.SetParent (HistoryDataParent, false);
		}
	}*/
    void DestroyData()
    {
        foreach (Transform child in HistoryDataParent)
        {
            Destroy(child.gameObject);
        }
    }
    #endregion

    #region COROUTINES



    #endregion


    #region GETTER_SETTER


    #endregion
}
