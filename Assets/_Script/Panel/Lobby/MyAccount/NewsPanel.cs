using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class NewsPanel : MonoBehaviour
{

    #region PUBLIC_VARIABLES

    //[Header ("Gamobjects")]

    [Header("Transforms")]
    public Transform NewsDataParent;

    [Header("ScriptableObjects")]
    public newsList NewsDataObj;

    public PanelNewsDetails NewsDetailsPanel;


    //[Header ("DropDowns")]


    //[Header ("Images")]


    //[Header("InputField")] 


    //	[Header ("Text")]


    //[Header ("Prefabs")]

    //[Header ("Enums")]


    //[Header ("Variables")]

    #endregion

    #region PRIVATE_VARIABLES

    #endregion

    #region UNITY_CALLBACKS
    // Use this for initialization
    void OnEnable()
    {
        NewsDetailsPanel.Close();

        UIManager.Instance.SocketGameManager.GetnewsBlog((socket, packet, args) =>
        {

            Debug.Log("GetnewsBlog  : " + packet.ToString());

            UIManager.Instance.HideLoader();

            JSONArray arr = new JSONArray(packet.ToString());
            string Source;

            Source = arr.getString(arr.length() - 1);

            var resp1 = Source;

            PokerEventListResponse<GetnewsBlogResult> resp = JsonUtility.FromJson<PokerEventListResponse<GetnewsBlogResult>>(resp1);

            if (resp.status.Equals(Constants.PokerAPI.KeyStatusSuccess))
            {
                for (int i = 0; i < resp.result.Count; i++)
                {
                    newsList PurchasehistoryDataList = Instantiate(NewsDataObj) as newsList;
                    string Title = resp.result[i].title;
                    string ShortDesc = resp.result[i].shortDesc;
                    string LongDesc = resp.result[i].longDesc;

                    PurchasehistoryDataList.SetData(Title, ShortDesc, LongDesc, i);
                    PurchasehistoryDataList.transform.SetParent(NewsDataParent, false);
                }
            }
            else
            {
                UIManager.Instance.DisplayMessagePanel(resp.message);
            }

        });

        //StaticDataDisplay ();
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
    void StaticDataDisplay()
    {

        for (int i = 0; i < 10; i++)
        {

            newsList PurchasehistoryDataList = Instantiate(NewsDataObj) as newsList;
            string Title = "Poker is a family";
            string ShortDesc = "Poker is a family of card games that combines gambling, strategy, and skill. All poker variants involve betting as an intrinsic part of play, and determine the winner of each hand.";
            string LongDesc = "Poker is a family of card games that combines gambling, strategy, and skill. All poker variants involve betting as an intrinsic part of play, and determine the winner of each hand.\nPoker is a family of card games that combines gambling, strategy, and skill. All poker variants involve betting as an intrinsic part of play, and determine the winner of each hand.";

            PurchasehistoryDataList.SetData(Title, ShortDesc, LongDesc, i);
            PurchasehistoryDataList.transform.SetParent(NewsDataParent, false);
        }
    }
    void DestroyData()
    {
        foreach (Transform child in NewsDataParent)
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
