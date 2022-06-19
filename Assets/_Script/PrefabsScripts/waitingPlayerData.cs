using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class waitingPlayerData : MonoBehaviour
{

    #region PUBLIC_VARIABLES 
    //[Header ("Gamobjects")]

    //[Header("Transforms")]


    //[Header ("ScriptableObjects")]


    //[Header ("DropDowns")]


    [Header("Images")]
    public Image profilePicture;

    [Header("Text")]
    public Text Position;
    public Text PlayerName;

    //[Header ("Prefabs")]

    //[Header ("Enums")]


    [Header("Variables")]
    public WaitingPlayerResultData mainData;

    #endregion 
    #region PRIVATE_VARIABLES 
    #endregion 
    #region UNITY_CALLBACKS     // Use this for initialization
    void OnEnable()
    {


    }
    void OnDisable()
    {

    }

    #endregion 
    #region DELEGATE_CALLBACKS 

    #endregion 
    #region PUBLIC_METHODS     public void SetData(WaitingPlayerResultData Data)
    {
        Reset();
        this.mainData = Data;
        Position.text = Data.position.ToString();
        PlayerName.text = Data.username.ToString();
        profilePicture.sprite = UIManager.Instance.assetOfGame.profileAvatarList.profileAvatarSprite[Data.profilePic];
        /* if (Data.avatar != null && !string.IsNullOrEmpty(Data.avatar))
         {
             string url = Constants.PokerAPI.IMAGE_RESIZE_URL + Constants.PokerAPI.BaseUrl + Data.avatar;
             Utility.Instance.DownloadImage(url, profilePicture, true);
         }
         else
         {
             profilePicture.sprite = UIManager.Instance.assetOfGame.profileAvatarList.profileAvatarSprite[Data.profilePic]; 
         }*/
    }


    #endregion 
    #region PRIVATE_METHODS     void Reset()
    {
        Position.text = "";
        PlayerName.text = "";
        profilePicture.sprite = UIManager.Instance.assetOfGame.SavedGamePlayData.spDefaultImage;
    }
    #endregion 
    #region COROUTINES 


    #endregion 

    #region GETTER_SETTER 

    #endregion 

}
