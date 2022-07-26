using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;


public class TournamentWinnerPanel : MonoBehaviour
{


    #region PUBLIC_VARIABLES

    //[Header ("Gamobjects")]

    //[Header ("Transforms")]


    [Header("ScriptableObjects")]
    public TournamentWinners[] WinnersList;


    //[Header ("DropDowns")]


    //[Header ("Images")]


    [Header("Text")]
    public TextMeshProUGUI MyRank;
    public TextMeshProUGUI Mywinning;
    public TextMeshProUGUI YouLose;


    //[Header ("Prefabs")]

    //[Header ("Enums")]


    //	[Header ("Variables")]
    public string[] Names = { "Plr1", "Plr2", "Plr3" };
    public int[] Scores = { 75, 15, 10 };


    #endregion

    #region PRIVATE_VARIABLES

    #endregion

    #region UNITY_CALLBACKS
    // Use this for initialization
    void OnEnable()
    {
        //		Reset ();
        //		SetWinnersData ();
        UIManager.Instance.HidePopup();
        Invoke("CloseObjects", 10f);
    }
    void OnDisable()
    {
        Reset();
    }
    /*
     // Update is called once per frame
     void Update()
     {

     }*/
    #endregion

    #region DELEGATE_CALLBACKS


    #endregion

    #region PUBLIC_METHODS
    public void SetWinnersData()
    {
        for (int i = 0; i < WinnersList.Length; i++)
        {
            WinnersList[i].Clear();
            WinnersList[i].WinnerName.text = Names[i];
            WinnersList[i].WinnerPrice.text = "$ " + Scores[i];
            WinnersList[i].Open();
        }
    }
    public void RegularTournamentData(List<RegularTournamentFinishedData> RegularTournamentFinishedData)
    {
        Reset();
        bool isExist = false;
        for (int i = 0; i < RegularTournamentFinishedData.Count; i++)
        {
            if (i <= 2)
            {
                WinnersList[i].WinnerName.text = RegularTournamentFinishedData[i].username;
                WinnersList[i].WinnerPrice.text = "$ " + RegularTournamentFinishedData[i].winningChips.ToString();
                WinnersList[i].Profile.sprite = UIManager.Instance.assetOfGame.profileAvatarList.profileAvatarSprite[RegularTournamentFinishedData[i].avatar];
                WinnersList[i].Open();
            }
            if (RegularTournamentFinishedData[i].id.Equals(UIManager.Instance.assetOfGame.SavedLoginData.PlayerId))
            {
                isExist = true;
                MyRank.text = "Your Place : " + (int)(i + 1);
                Mywinning.text = "Your Winning : $ " + RegularTournamentFinishedData[i].winningChips.ToString();
                MyRank.Open();
                Mywinning.Open();
                MyRank.transform.parent.gameObject.SetActive(true);
            }
            else
            {
                YouLose.Close();
            }
        }
        if (!isExist)
        {
            YouLose.text = "You Lose !";
            YouLose.Open();
            YouLose.transform.parent.gameObject.SetActive(true);
        }
        this.Open();
    }

    public void SNGTournamentData(List<RegularTournamentFinishedData> RegularTournamentFinishedData)
    {
        Reset();
        for (int i = 0; i < RegularTournamentFinishedData.Count; i++)
        {
            WinnersList[i].WinnerName.text = RegularTournamentFinishedData[i].username;
            WinnersList[i].WinnerPrice.text = "$ " + RegularTournamentFinishedData[i].winningChips.ToString();
            WinnersList[i].Profile.sprite = UIManager.Instance.assetOfGame.profileAvatarList.profileAvatarSprite[RegularTournamentFinishedData[i].avatar];
            WinnersList[i].Open();
        }
        this.Open();
    }
    #endregion

    #region PRIVATE_METHODS
    void CloseObjects()
    {
        StartCoroutine(NextScreen(1.5f));
    }
    void Reset()
    {
        foreach (TournamentWinners Wnr in WinnersList)
        {
            Wnr.Clear();
            Wnr.Close();
        }
        YouLose.text = "";
        Mywinning.text = "";
        MyRank.text = "";
        YouLose.Close();
        Mywinning.Close();
        MyRank.Close();
    }
    #endregion

    #region COROUTINES
    IEnumerator NextScreen(float timer)
    {
        UIManager.Instance.DisplayLoader("");
        UIManager.Instance.GameScreeen.Close();
        yield return new WaitForSeconds(timer);
        this.Close();
        UIManager.Instance.LobbyPanelNew.Open(); // LobbyScreeen not used more
    }

    #endregion


    #region GETTER_SETTER


    #endregion



}
