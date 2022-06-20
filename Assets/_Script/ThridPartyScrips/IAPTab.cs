using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Purchasing;
using System.Text;
using System;
using UnityEngine.UI;
using TMPro;
using Constants;
using System.Security.Permissions;

public class IAPTab : MonoBehaviour
{

    public TextMeshProUGUI chipsText;
    public Text priceText;

    private IAPData _iapData;

    public IAPData iapData
    {
        get
        {
            return _iapData;
        }
        set
        {
            _iapData = value;
            chipsText.text = iapData.chips + String.Empty;
        }
    }

    void Awake()
    {
    }

    void Start()
    {

    }

    public void ChangePriceValue(string price)
    {
        priceText.text = "Buy " + price;
    }

    public void OnPurchaseButtonClicked()
    {
        UnityIAPManager.instance.BuyProductID(iapData.in_app_purchase_id);
    }

    public void OnPurchaseComplete(Product product)
    {
        Debug.Log("!!!!!!!!!!!!!!!!!!!!!!!!!!!! Get chips....." + iapData.chips);

        //		UIManager.Instance.SocketGameManager.InAppPurchaseSuccess (product, iapData.price, iapData.chips, (socket, packet, args) => {
        //			//Handle on success.....
        //			Debug.Log ("!!!!!!!!packet result....... " + packet.ToString ());
        //			JSONArray arr = new JSONArray (packet.ToString ());
        //			string Source;
        //			Source = arr.getString (arr.length () - 1);
        //			var resp1 = Source;
        //			PokerEventResponse<IAPGettingResponseData> iapGettingResponseData = JsonUtility.FromJson<PokerEventResponse<IAPGettingResponseData>> (resp1);
        //			UIManager.Instance.assetOfGame.SavedLoginData.chips = iapGettingResponseData.result.totalChips;
        //			UIManager.Instance.LobbyScreeen.txtChips.text = UIManager.Instance.assetOfGame.SavedLoginData.chips.ConvertToCommaSeparatedValue ();
        //			UIManager.Instance.DisplayMessagePanel (iapGettingResponseData.message, () => {
        //				UIManager.Instance.HidePopup ();
        //			});
        //			Debug.Log ("!!!!!!!!! total chips...... " + iapGettingResponseData.result.totalChips);
        //
        //		});

        Utility.Instance.IAPCall(product, iapData.price, iapData.chips);
    }

    public void OnPurchaseFail(Product product, PurchaseFailureReason failureReason)
    {
        //UIManager.Instance.DisplayMessagePanelOnly ("Sorry something went wrong");
        Debug.Log("%%%%%%%%%%%% On Purchase fail...... " + failureReason.ToString());
    }
}


