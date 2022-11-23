using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

[CreateAssetMenu(fileName = "Data", menuName = "Inventory/List", order = 1)]
public class AssetOfGame : ScriptableObject
{
    //	[SerializeField]

    //	public List<Theme> theme;
    public LoginSaved SavedLoginData;
    public GamePlay SavedGamePlayData;
    public Cards PokerCards;

    public ProfileAvatarList profileAvatarList;
    //public GameObjects SettedGameObjects;
}

[Serializable]
public class LoginSaved
{
    public string Username;
    public string password;
    public string phoneCode;
    public string phoneNumber;
    public bool isRememberMe;
    public bool isCash;
    public bool isInAppPurchaseAllowed;
    public string PlayerId;
    public double chips;
    public string accountNumber;
    public int SelectedAvatar;
    public string fcmRegistrationToken;
    public string userUuid;
    public string mobile;
    public bool IsLogin;
    public bool isSuperPlayer;
    public bool isAbsolute;
    public string timeZone;
    public double cash;

    // Registration....
    public string publicKey;
    public int[] privateKey;
}

[Serializable]
public class GamePlay
{
    public string TableId;
    public string PlayerId;
    public Sprite spDefaultImage;
}

[Serializable]
public class Cards
{
    public Sprite[] spadesCardsList;
    public Sprite[] diamondsCardsList;
    public Sprite[] heartsCardsList;
    public Sprite[] clubsCardsList;
    public Sprite BackCard;
}

[Serializable]
public class ProfileAvatarList
{
    public Sprite[] profileAvatarSprite;
}