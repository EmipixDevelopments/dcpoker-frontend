using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectedAvatarPnel : MonoBehaviour
{


    #region PUBLIC_VARIABLES

    [Header("Gamobjects")]
    public GameObject[] SelectedAvatarTabs;

    //[Header ("Transforms")]


    //[Header ("ScriptableObjects")]


    //[Header ("DropDowns")]


    //[Header ("Images")]


    //[Header ("Text")]


    //[Header ("Prefabs")]

    //[Header ("Enums")]


    [Header("Variables")]

    public int selectedAvatar;
    #endregion

    #region PRIVATE_VARIABLES

    #endregion

    #region UNITY_CALLBACKS
    // Use this for initialization
    void OnEnable()
    {
        Reset();

    }
    //	void OnDisable()
    //	{
    //
    //	}
    //	// Update is called once per frame
    //	void Update ()
    //	{
    //
    //	}
    #endregion

    #region DELEGATE_CALLBACKS


    #endregion

    #region PUBLIC_METHODS
    public void ChangeAvatarBtnTap(bool IsOPen)
    {
        //		if (IsOPen) 
        //		{
        //			gameObject.SetActive(true);
        //		}
        //		else 
        //		{
        //			gameObject.SetActive(false);
        //		}
        gameObject.SetActive(IsOPen);
    }

    public void ProfileImageSet(int id)
    {
        Debug.Log("Id => " + id);

        UIManager.Instance.assetOfGame.SavedLoginData.SelectedAvatar = id;
        selectedAvatar = id;
        Reset();
    }
    public void SelectButtonTap()
    {
        UIManager.Instance.SoundManager.OnButtonClick();
        UIManager.Instance.SocketGameManager.GetplayerProfilePic(selectedAvatar,null, (socket, packet, args) =>
        {

            Debug.Log("GetplayerProfilePic  : " + packet.ToString());

            UIManager.Instance.HideLoader();

            JSONArray arr = new JSONArray(packet.ToString());
            string Source;
            Source = arr.getString(arr.length() - 1);
            var resp1 = Source;

            PokerEventResponse resp = JsonUtility.FromJson<PokerEventResponse>(resp1);

            if (resp.status.Equals(Constants.PokerAPI.KeyStatusSuccess))
            {
                UIManager.Instance.ProfilePic = UIManager.Instance.assetOfGame.SavedLoginData.SelectedAvatar;
            }
            else
            {
                UIManager.Instance.DisplayMessagePanel(resp.message);
            }
        });
    }

    #endregion

    #region PRIVATE_METHODS
    void Reset()
    {
        foreach (GameObject Go in SelectedAvatarTabs)
        {
            Go.SetActive(false);
        }
        selectedAvatar = UIManager.Instance.assetOfGame.SavedLoginData.SelectedAvatar;
        SelectedAvatarTabs[UIManager.Instance.assetOfGame.SavedLoginData.SelectedAvatar].SetActive(true);
    }
    #endregion

    #region COROUTINES

    #endregion

    #region GETTER_SETTER

    #endregion
}
