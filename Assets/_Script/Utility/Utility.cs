using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Collections.Generic;
using System;
using System.Text;
using System.Text.RegularExpressions;
using System.Linq;
using TMPro;
using UnityEngine.Purchasing;
//using UnityEngine.Purchasing;

public class Utility : MonoBehaviour
{
    #region PUBLIC_VARIABLES

    public static Utility Instance;

    #endregion

    #region PRIVATE_VARIABLES

    #endregion

    #region DELEGATE_CALLBACKS

    void Awake()
    {
        Instance = this;
    }

    #endregion

    #region PUBLIC_METHODS

    public void MoveObject(Transform obj, Vector3 fromPos, Vector3 toPos, float time)
    {
        StartCoroutine(MoveObjectSmoothly(obj, fromPos, toPos, time));
    }

    public void MoveObject(Transform obj, Vector3 toPos, float time)
    {
        StartCoroutine(MoveObjectSmoothly(obj, obj.position, toPos, time));
    }

    public void RotateObject(Transform obj, Vector3 fromRotation, Vector3 toRotation, float time)
    {
        StartCoroutine(RotateObjectSmoothly(obj, fromRotation, toRotation, time));
    }

    public void DownloadImage(string url, Image imgSource, bool displayLoader = true)
    {
        StartCoroutine(GetImage(url, imgSource, displayLoader));
    }

    public string GetPacketString(BestHTTP.SocketIO.Packet packet)
    {
        JSONArray arr = new JSONArray(packet.ToString());
        return arr.getString(arr.length() - 1);
    }

    public void CheckHebrewOwn(Text TextStuff, string msg)
    {
        bool isHebrew = false;
        bool isFirstHebrew = false;
        bool isprevious = false;
        char[] separators = new char[] { ' ', '.' };
        string example = "This is an example.";
        string str1 = msg;
        string pattern = @"[\p{IsHebrew} ]+";
        string newstat = "";
        string abc = "";
        string updatedStat = "";
        foreach (var word in str1.Split(separators, StringSplitOptions.RemoveEmptyEntries))
        {
            var hebrewMatchCollection = Regex.Matches(word.ToString(), pattern);
            string hebrewPart = string.Join(" ", hebrewMatchCollection.Cast<Match>().Select(m => m.Value));
            string tempLetter = Regex.Replace(word.ToString(), "[^a-zA-Z]", "");
            if (hebrewPart != "")
            {
                isFirstHebrew = true;
                abc = Reverse(word);
                updatedStat += " " + abc;
                isprevious = true;
            }
            else if (tempLetter != "")
            {
                isFirstHebrew = false;
                isHebrew = false;
                abc = word;
                updatedStat += abc + " ";
                isprevious = false;
            }
            else
            {
                if (isFirstHebrew)
                    abc = Reverse(word);
                else
                    abc = word;
            }
            if (isFirstHebrew)
            {
                newstat = abc + " " + newstat;
            }
            else
            {
                newstat += " " + abc;
            }
            TextStuff.text = newstat;
            //            Debug.Log("table : " + newstat);
        }
    }
    public static string Reverse(string s)
    {
        char[] charArray = s.ToCharArray();
        Array.Reverse(charArray);
        return new string(charArray);
    }
    public void UpdateHorizontalLayout(GameObject gameObject)
    {
        StartCoroutine(UpdateHorizontalLayoutIenum(gameObject));
    }

    IEnumerator UpdateHorizontalLayoutIenum(GameObject gameObject)
    {
        yield return new WaitForEndOfFrame();
        try
        {
            ContentSizeFitter contentSizeFitter = gameObject.transform.parent.GetComponent<ContentSizeFitter>();
            if (contentSizeFitter)
            {
                StartCoroutine(UpdateHorizontalLayoutEnumerator(contentSizeFitter));
            }
        }
        catch (Exception e)
        {
            print(e);
        }
    }
    private IEnumerator UpdateHorizontalLayoutEnumerator(ContentSizeFitter contentSizeFitter)
    {
        yield return new WaitForEndOfFrame();
        try
        {
            Canvas.ForceUpdateCanvases();
            contentSizeFitter.horizontalFit = ContentSizeFitter.FitMode.PreferredSize;
            if (contentSizeFitter)
                contentSizeFitter.SetLayoutHorizontal();
            LayoutRebuilder.ForceRebuildLayoutImmediate((RectTransform)contentSizeFitter.transform);
            Canvas.ForceUpdateCanvases();
        }
        catch (Exception e)
        {

        }
    }
    public GameType GetGameFormatFromString(string gameFormatString)
    {
        if (gameFormatString == "tournament")
        {
            return GameType.Touranment;
        }
        else if (gameFormatString == "sng")
        {
            return GameType.sng;
        }
        else
        {
            return GameType.cash;
        }
    }

    public Sprite GetCard(string card)
    {
        if (card == "BC")
            return UIManager.Instance.assetOfGame.PokerCards.BackCard;

        string rank = card.Substring(0, 1);
        string suit = card.Substring(1, 1);

        PokerCardSuit pokerCardSuit = suit.ToEnum<PokerCardSuit>();
        int pokerCardRank = GetCardRank(rank);

        //		List<Sprite> cardsList = null;
        switch (pokerCardSuit)
        {
            case PokerCardSuit.S:
                return UIManager.Instance.assetOfGame.PokerCards.spadesCardsList[pokerCardRank];
            case PokerCardSuit.D:
                return UIManager.Instance.assetOfGame.PokerCards.diamondsCardsList[pokerCardRank];
            case PokerCardSuit.H:
                return UIManager.Instance.assetOfGame.PokerCards.heartsCardsList[pokerCardRank];
            case PokerCardSuit.C:
                return UIManager.Instance.assetOfGame.PokerCards.clubsCardsList[pokerCardRank];
        }
        return null;
    }

    public void DistributeCard(Transform obj, Vector3 fromPos, Vector3 toPos, float time, Action action)
    {
        StartCoroutine(DistributeCardAnimation(obj, fromPos, toPos, time, action));
    }

    public float GetHandRankStregthMeterValue(string handRank)
    {
        PokerHandRank phr = handRank.ToEnum<PokerHandRank>();

        return (int)phr / 10f;
    }

    public string GetHandRank(string handRank)
    {
        return AddSpacesToSentence(handRank, true).ToUpper();
    }

    public string GetDecimalStringValue(double value)
    {
        return (Math.Round(value, 3)).ToString();
    }

    public float GetDecimalFloatValue(float value)
    {
        float ff;
        ff = Mathf.Round(value * 100) / 100;
        return ff;
    }

    public string GetDecimalzeroStringValue(double value)
    {
        return (Math.Round(value, 0)).ToString();
    }

    public string GetActionName(int actionNumber)
    {
        string actionString = "";

        if (actionNumber == 0)
        {
            actionString = "Small Blind";
        }
        else if (actionNumber == 1)
        {
            actionString = "Big Blind";
        }
        else if (actionNumber == 2)
        {
            actionString = "Check";
        }
        else if (actionNumber == 3)
        {
            actionString = "Bet";
        }
        else if (actionNumber == 4)
        {
            actionString = "Call";
        }
        else if (actionNumber == 5)
        {
            actionString = "";
        }
        else if (actionNumber == 6)
        {
            actionString = "Fold";
        }
        else if (actionNumber == 7)
        {
            actionString = "Timeout";
        }
        else if (actionNumber == 8)
        {
            actionString = "All-in";
        }
        else if (actionNumber == 9)
        {
            actionString = "Straddle";
        }
        else
        {
            actionString = "";
        }

        return actionString;
    }

    public string GetShortenName(string name)
    {
        if (name.Length > 8)
        {
            return name.Substring(0, 8) + "..";
        }
        else
        {
            return name;
        }
    }

    public string GetOSName()
    {
#if UNITY_ANDROID
        return "android";
#elif UNITY_IOS
        return "ios";
#else
        return "other";
#endif
    }
    public string GetDeviceIdForOsBased()
    {

        if (Application.platform == RuntimePlatform.WebGLPlayer)
        {
            return UIManager.Instance.webglToken;
        }
        else
        {
            return SystemInfo.deviceUniqueIdentifier.ToString();
        }

    }
    public string GetApplicationVersion()
    {
        return Application.version;
    }

    public string GetApplicationVersionWithOS()
    {
#if UNITY_EDITOR
        return "v" + Application.version + "u";
#elif UNITY_ANDROID
		return "v" + Application.version + "a";	
#elif UNITY_IOS
		return "v" + Application.version + "i";	
#else
        return "";
#endif
    }

    public Sprite GetAvatarById(int index)
    {
        if (index >= UIManager.Instance.assetOfGame.profileAvatarList.profileAvatarSprite.Length)
            return UIManager.Instance.assetOfGame.profileAvatarList.profileAvatarSprite[0];
        else
            return UIManager.Instance.assetOfGame.profileAvatarList.profileAvatarSprite[index];
    }

    public RoomsListing.Room GetNewRoomObjectClone(RoomsListing.Room roomObject)
    {
        RoomsListing.Room newCloneObject = new RoomsListing.Room();

        newCloneObject.tournamentId = roomObject.tournamentId;
        newCloneObject.roomId = roomObject.roomId;
        newCloneObject.roomName = roomObject.roomName;
        newCloneObject.status = roomObject.status;
        newCloneObject.stake = roomObject.stake;
        newCloneObject.currencyType = roomObject.currencyType;
        newCloneObject.type = roomObject.type;
        newCloneObject.playerCount = roomObject.playerCount;
        Debug.Log("player Count : " + roomObject.playerCount + " / / / " + newCloneObject.playerCount);
        newCloneObject.maxPlayers = roomObject.maxPlayers;
        newCloneObject.pot = roomObject.pot;
        newCloneObject.minBuyIn = roomObject.minBuyIn;
        newCloneObject.maxBuyIn = roomObject.maxBuyIn;
        newCloneObject.gameLimit = roomObject.gameLimit;
        newCloneObject.isLimitGame = roomObject.isLimitGame;
        newCloneObject.smallBlind = roomObject.smallBlind;
        newCloneObject.bigBlind = roomObject.bigBlind;
        newCloneObject.isTournament = roomObject.isTournament;
        newCloneObject.isPasswordProtected = roomObject.isPasswordProtected;
        newCloneObject.isGPSRestriction = roomObject.isGPSRestriction;
        newCloneObject.isIPAddressRestriction = roomObject.isIPAddressRestriction;
        newCloneObject.namespaceString = roomObject.namespaceString;
        newCloneObject.pokerGameType = roomObject.pokerGameType;
        newCloneObject.pokerGameFormat = roomObject.pokerGameFormat;

        newCloneObject.tableNumber = roomObject.tableNumber;
        newCloneObject.name = roomObject.name;
        newCloneObject.muck = roomObject.muck;

        return newCloneObject;
    }

    public void OpenLink(string url)
    {
#if UNITY_WEBGL && !UNITY_EDITOR
			ExternalCallClass.Instance.OpenUrl(url);
			//Application.ExternalEval("window.open(\"" + url + "\",\"_blank\")");
#else
        Application.OpenURL(url);
#endif
    }

    public void ClearLoginData()
    {
        PlayerPrefs.SetString("USERNAME", "");
        PlayerPrefs.SetString("PASSWORD", "");
        PlayerPrefs.SetInt("REMEMBER_ME", 0);
        PlayerPrefs.SetInt("AutoLogin", 0);

    }
    #endregion

    #region PRIVATE_METHODS

    private static int GetCardRank(string rank)
    {
        int cardRank = 0;
        if (rank.Equals("A"))
            cardRank = 12;
        else if (rank.Equals("K"))
            cardRank = 11;
        else if (rank.Equals("Q"))
            cardRank = 10;
        else if (rank.Equals("J"))
            cardRank = 9;
        else if (rank.Equals("T"))
            cardRank = 8;
        else
            cardRank = (int.Parse(rank)) - 2;

        return cardRank;
    }

    private string AddSpacesToSentence(string text, bool preserveAcronyms)
    {
        if (string.IsNullOrEmpty(text))
            return string.Empty;
        StringBuilder newText = new StringBuilder(text.Length * 2);
        newText.Append(text[0]);
        for (int i = 1; i < text.Length; i++)
        {
            if (char.IsUpper(text[i]))
                if ((text[i - 1] != ' ' && !char.IsUpper(text[i - 1])) ||
                    (preserveAcronyms && char.IsUpper(text[i - 1]) &&
                    i < text.Length - 1 && !char.IsUpper(text[i + 1])))
                    newText.Append(' ');
            newText.Append(text[i]);
        }
        return newText.ToString();
    }

    #endregion

    #region COROUTINES

    private IEnumerator MoveObjectSmoothly(Transform obj, Vector3 fromPos, Vector3 toPos, float time)
    {
        float i = 0;

        while (i < 1)
        {
            i += Time.deltaTime * (1 / time);
            if (obj != null && obj.gameObject.activeInHierarchy)
            {
                obj.position = Vector3.Lerp(fromPos, toPos, i);
            }
            yield return 0;
        }
    }

    private IEnumerator RotateObjectSmoothly(Transform obj, Vector3 fromRotation, Vector3 toRotation, float time)
    {
        float i = 0;

        while (i < 1)
        {
            i += Time.deltaTime * (1 / time);
            obj.eulerAngles = Vector3.Lerp(fromRotation, toRotation, i);
            yield return 0;
        }

        obj.eulerAngles = toRotation;
    }

    private IEnumerator GetImage(string url, Image imgSource, bool displayLoader)
    {
        GameObject loader = null;
        if (displayLoader)
        {

            // Instantiate the loader.
            loader = Instantiate(UIManager.Instance.downloadImageLoaderPrefab) as GameObject;

            // Make child of the Image.
            loader.transform.SetParent(imgSource.transform, false);
            loader.transform.localPosition = Vector3.zero;
        }

        WWW www = new WWW(url);

        yield return www;

        //	Destroys the loader object on process complete.
        Destroy(loader);

        if (www.error == null && www.texture != null)
        {
            Sprite sp = Sprite.Create(www.texture, new Rect(0, 0, www.texture.width, www.texture.height), Vector2.zero);
            imgSource.sprite = sp;
        }
        else
        {
            //	If there is error in retrieving the image, display default sprite.
            imgSource.sprite = UIManager.Instance.assetOfGame.SavedGamePlayData.spDefaultImage;
        }
    }

    private IEnumerator DistributeCardAnimation(Transform obj, Vector3 fromPos, Vector3 toPos, float time, Action action)
    {
        float i = 0;

        while (i < 1)
        {
            i += Time.deltaTime * (1 / time);
            obj.position = Vector3.Lerp(fromPos, toPos, i);
            //ravi mehta
            //			obj.localScale = Vector3.Lerp (Vector3.zero, Vector3.one, i);

            yield return 0;
        }

        action();
    }

    #endregion

    #region IAPCalls

    //public void IAPCall (Product product, decimal price, int chips)
    //{
    //	StartCoroutine (IAPCallIEnum (product, price, chips));
    //}

    //private IEnumerator IAPCallIEnum (Product product, decimal price, int chips)
    //{
    //	yield return new WaitForSeconds (1);
    //	while (Game.Lobby.socketManager.State != BestHTTP.SocketIO.SocketManager.States.Open) {
    //		print ("Game.Lobby.socketManager.State: " + Game.Lobby.socketManager.State.ToString ());
    //		yield return new WaitForSeconds (1);
    //	}

    //	UIManager.Instance.SocketGameManager.InAppPurchaseSuccess (product, price, chips, (socket, packet, args) => {
    //		Debug.Log ("!!!!!!!!packet result....... " + packet.ToString ());
    //		JSONArray arr = new JSONArray (packet.ToString ());
    //		string Source;
    //		Source = arr.getString (arr.length () - 1);
    //		var resp1 = Source;
    //		PokerEventResponse<IAPGettingResponseData> iapGettingResponseData = JsonUtility.FromJson<PokerEventResponse<IAPGettingResponseData>> (resp1);
    //		UIManager.Instance.assetOfGame.SavedLoginData.chips = iapGettingResponseData.result.totalChips;
    //		UIManager.Instance.LobbyScreeen.txtChips.text = UIManager.Instance.assetOfGame.SavedLoginData.chips.ConvertToCommaSeparatedValueColor ();
    //		UIManager.Instance.LobbyScreeen.PanelMyAccount.ProfilePanel.Chips = iapGettingResponseData.result.totalChips;
    //		UIManager.Instance.DisplayMessagePanel (iapGettingResponseData.message, () => {
    //			UIManager.Instance.HidePopup ();
    //		});
    //	});
    //}

    #endregion

    #region IAPCalls

    public void IAPCall(Product product, float price, int chips)
    {
        StartCoroutine(IAPCallIEnum(product, price, chips));
    }

    private IEnumerator IAPCallIEnum(Product product, float price, int chips)
    {
        yield return new WaitForSeconds(1);
        while (Game.Lobby.socketManager.State != BestHTTP.SocketIO.SocketManager.States.Open)
        {
            print("Game.Lobby.socketManager.State: " + Game.Lobby.socketManager.State.ToString());
            yield return new WaitForSeconds(1);
        }

        UIManager.Instance.SocketGameManager.InAppPurchaseSuccess(product, price, chips, (socket, packet, args) =>
        {
            Debug.Log("!!!!!!!!packet result....... " + packet.ToString());
            JSONArray arr = new JSONArray(packet.ToString());
            string Source;
            Source = arr.getString(arr.length() - 1);
            var resp1 = Source;
            PokerEventResponse<IAPGettingResponseData> iapGettingResponseData = JsonUtility.FromJson<PokerEventResponse<IAPGettingResponseData>>(resp1);
            UIManager.Instance.assetOfGame.SavedLoginData.chips = iapGettingResponseData.result.totalChips;
            UIManager.Instance.LobbyScreeen.txtChips.text = UIManager.Instance.assetOfGame.SavedLoginData.chips.ConvertToCommaSeparatedValueColor();
            UIManager.Instance.LobbyScreeen.ProfileScreen.PanelMyAccount.ProfilePanel.Chips = iapGettingResponseData.result.totalChips;
            UIManager.Instance.DisplayMessagePanel(iapGettingResponseData.message, () =>
            {
                UIManager.Instance.HidePopup();
            });
        });
    }

    #endregion
    #region GETTER_SETTER
    public string CurrentDomain
    {
        get
        {
            if (UIManager.Instance.server == SERVER.Custom)
                return PlayerPrefs.GetString("CUSTOM_URL");
            else
                return Constants.PokerAPI.BaseUrl;
        }
    }
    #endregion
}

public static class MyExtension
{
    /// <summary>
    /// Convert string to camel case.
    /// </summary>
    /// <returns>The camel case string.</returns>
    /// <param name="str">String.</param>
    public static string ToPascalCase(this string str)
    {
        string[] words = str.Split(' ');
        string newString = "";

        foreach (string s in words)
        {
            newString += s.ToCharArray()[0].ToString().ToUpper() + s.Substring(1) + " ";
        }

        return newString;
    }

    /// <summary>
    /// Converts amount to comma separated value.
    /// </summary>
    /// <returns>The to comma separated value.</returns>
    /// <param name="amount">Amount.</param>
    public static string ConvertToCommaSeparatedValue(this long amount)
    {
        return amount.ToString("#,##0");
    }
    public static string ConvertToCommaSeparatedValueColor(this double value)
    {
        string amt = value.ToString("###,###,##0.00");

        string[] splitString = amt.Split('.');
        string newString = splitString[0];

        if (splitString.Length > 1)
        {
            newString += "<color=\"yellow\">." + splitString[1] + "</color>";
            //newString += "<color=#FF000A>.<size=80%>"+ splitString[1] + "</size></color>"; //color code example
        }
        return newString;
    }
    /// <summary>
    /// Open the specified component.
    /// </summary>
    /// <param name="component">Component.</param>
    public static void Open(this MonoBehaviour component)
    {
        if (component.gameObject != null)
            component.gameObject.SetActive(true);
    }

    /// <summary>
    /// Close the specified component.
    /// </summary>
    /// <param name="component">Component.</param>
    public static void Close(this MonoBehaviour component)
    {
        if (component.gameObject != null)
            component.gameObject.SetActive(false);
    }

    /// <summary>
    /// Converts to comma separated value.
    /// </summary>
    /// <returns>The to comma separated value.</returns>
    /// <param name="value">Value.</param>
    public static string ConvertToCommaSeparatedValue(this double value)
    {
        string amt = value.ToString("###,###,##0.00");
        //amt = "$" + amt;
        return amt;//.Replace (',', '.');
    }

    public static string ConvertToCommaSeparatedValueBuyIn(this long value)
    {
        string amt = value.ToString("###,##0.00");
        //		amt =  amt;
        return amt;//.Replace (',', '.');
    }

    public static string ConvertToCommaSeparatedValue(this int value)
    {
        string amt = value.ToString("###,###,###");
        //amt = "$" + amt;
        return amt;//.Replace (',', '.');
    }

    public static string ConvertToCommaSeparatedValue(this float value)
    {
        string amt = value.ToString("###,###,##0.00");
        return amt;//.Replace (',', '.');
    }

    public static string ConvertDoubleDecimalsToValue(this double value)
    {
        string amt = value.ToString("###,###,##0.00");
        return amt;//.Replace (',', '.');
    }

    public static string ConvertFloatDecimalsToValue(this float value)
    {
        string amt = value.ToString("###,###,##0.00");
        return amt;//.Replace (',', '.');
    }

    public static Double ConvertDoubleDecimals(this double value)
    {
        //		double amt = value.ToString ("0.00");
        double amt = Math.Round(value * 100.0) / 100.0;
        return amt;//.Replace (',', '.');
    }

    public static string ConvertUserNameToStarString(this string value)
    {
        /*for (int i = 1; i < value.Length - 1; i++) {	
			value = value.Remove (i, 1);
			value = value.Insert (i, "*");
		}*/
        return value;
    }

    public static string GetDateFromString(this string value)
    {
        double temp = double.Parse(value);

        System.DateTime dateTime = new System.DateTime(1970, 1, 1, 0, 0, 0, 0);
        dateTime = dateTime.AddMilliseconds(temp);

        //		return dateTime.ToString ("yyyy/M/d");
        return dateTime.ToString("dd/MM");
    }

    public static string GetTimeFromString(this string value)
    {
        double temp = double.Parse(value);

        System.DateTime dateTime = new System.DateTime(1970, 1, 1, 0, 0, 0, 0);
        dateTime = dateTime.AddMilliseconds(temp);

        return dateTime.ToString("hh:mm");
    }

    public static string UTCTimeStringToLocalTime(this string value)
    {
        DateTime utcDateTime = DateTime.Parse(value);
        return utcDateTime.ToLocalTime().ToString();
    }

    /// <summary>
    /// Rounds to.
    /// </summary>
    /// <returns>The to.</returns>
    /// <param name="value">Value.</param>
    /// <param name="roundToValue">Round to value.</param>
    public static float RoundTo(this float value, float roundToValue)
    {
        return (float)Math.Round(value / roundToValue) * roundToValue;
    }

    /// <summary>
    /// Rounds to.
    /// </summary>
    /// <returns>The to.</returns>
    /// <param name="value">Value.</param>
    /// <param name="roundToValue">Round to value.</param>
    public static long RoundTo(this long value, long roundToValue)
    {
        return (long)Mathf.Round(value / roundToValue) * roundToValue;
    }


    /// <summary>
    /// Converts to the enum.
    /// </summary>
    /// <returns>The enum.</returns>
    /// <param name="value">Value.</param>
    /// <typeparam name="T">The 1st type parameter.</typeparam>
    public static T ToEnum<T>(this string value)
    {
        return (T)Enum.Parse(typeof(T), value, true);
    }

    #region Number To Short String Format

    private static readonly int charA = Convert.ToInt32('a');

    private static readonly Dictionary<int, string> units = new Dictionary<int, string> {
        { 0, "" },
        { 1, "K" },
        { 2, "M" },
        { 3, "B" },
        { 4, "T" }
    };

    public static string FormatNumberUS(this long value)
    {
        Debug.Log("value => " + value);

        if (value < 1d)
        {
            Debug.Log("if value => " + value);

            return "0";
        }

        var n = (int)Math.Log(value, 1000);
        var m = value / Math.Pow(1000, n);
        var unit = "";

        Debug.Log(" value set1 => " + n);
        Debug.Log(" value set2 => " + m);

        if (n < units.Count)
        {
            unit = units[n];
        }
        else
        {
            var unitInt = n - units.Count;
            var secondUnit = unitInt % 26;
            var firstUnit = unitInt / 26;
            unit = Convert.ToChar(firstUnit + charA).ToString() + Convert.ToChar(secondUnit + charA).ToString();
        }

        // Math.Floor(m * 100) / 100) fixes rounding errors
        return (Math.Floor(m * 100) / 100)/*.ToString("0.##")*/ + unit;
    }

    public static string FormatNumberUS(this double value)
    {
        if (value < 1d)
        {
            return "0";
        }

        var n = (int)Math.Log(value, 1000);
        var m = value / Math.Pow(1000, n);
        var unit = "";

        if (n < units.Count)
        {
            unit = units[n];
        }
        else
        {
            var unitInt = n - units.Count;
            var secondUnit = unitInt % 26;
            var firstUnit = unitInt / 26;
            unit = Convert.ToChar(firstUnit + charA).ToString() + Convert.ToChar(secondUnit + charA).ToString();
        }

        // Math.Floor(m * 100) / 100) fixes rounding errors
        return (Math.Floor(m * 100) / 100)/*.ToString("0.##")*/ + unit;
    }

    public static string DoubleToFloatToString(this double value)
    {
        float newValue = (float)value;

        return Math.Round(newValue, 2).ToString();
    }

    public static double floatToDouble(this float value)
    {
        return double.Parse(value.ToString());
    }

    public static float doubleToFloat(this double value)
    {
        return float.Parse(value.ToString());
    }

    #endregion
}

#region Dynamic Transform
/* Demo Code
 _ImgTransform.SetAnchor(AnchorPresets.TopRight);
 _ImgTransform.SetAnchor(AnchorPresets.TopRight,-10,-10);
 
 ImgTransform.SetPivot(PivotPresets.TopRight);

RectTransformExtensions.SetAnchor (textProductId.GetComponent<RectTransform> (), AnchorPresets.StretchAll, 0, 0);
*/
public enum AnchorPresets
{
    TopLeft,
    TopCenter,
    TopRight,

    MiddleLeft,
    MiddleCenter,
    MiddleRight,

    BottomLeft,
    BottonCenter,
    BottomRight,
    BottomStretch,

    VertStretchLeft,
    VertStretchRight,
    VertStretchCenter,

    HorStretchTop,
    HorStretchMiddle,
    HorStretchBottom,

    StretchAll
}

public enum PivotPresets
{
    TopLeft,
    TopCenter,
    TopRight,

    MiddleLeft,
    MiddleCenter,
    MiddleRight,

    BottomLeft,
    BottomCenter,
    BottomRight,
}

public static class RectTransformExtensions
{
    public static void SetAnchor(this RectTransform source, AnchorPresets allign, int offsetX = 0, int offsetY = 0)
    {
        source.anchoredPosition = new Vector3(offsetX, offsetY, 0);

        switch (allign)
        {
            case (AnchorPresets.TopLeft):
                {
                    source.anchorMin = new Vector2(0, 1);
                    source.anchorMax = new Vector2(0, 1);
                    break;
                }
            case (AnchorPresets.TopCenter):
                {
                    source.anchorMin = new Vector2(0.5f, 1);
                    source.anchorMax = new Vector2(0.5f, 1);
                    break;
                }
            case (AnchorPresets.TopRight):
                {
                    source.anchorMin = new Vector2(1, 1);
                    source.anchorMax = new Vector2(1, 1);
                    break;
                }

            case (AnchorPresets.MiddleLeft):
                {
                    source.anchorMin = new Vector2(0, 0.5f);
                    source.anchorMax = new Vector2(0, 0.5f);
                    break;
                }
            case (AnchorPresets.MiddleCenter):
                {
                    source.anchorMin = new Vector2(0.5f, 0.5f);
                    source.anchorMax = new Vector2(0.5f, 0.5f);
                    break;
                }
            case (AnchorPresets.MiddleRight):
                {
                    source.anchorMin = new Vector2(1, 0.5f);
                    source.anchorMax = new Vector2(1, 0.5f);
                    break;
                }

            case (AnchorPresets.BottomLeft):
                {
                    source.anchorMin = new Vector2(0, 0);
                    source.anchorMax = new Vector2(0, 0);
                    break;
                }
            case (AnchorPresets.BottonCenter):
                {
                    source.anchorMin = new Vector2(0.5f, 0);
                    source.anchorMax = new Vector2(0.5f, 0);
                    break;
                }
            case (AnchorPresets.BottomRight):
                {
                    source.anchorMin = new Vector2(1, 0);
                    source.anchorMax = new Vector2(1, 0);
                    break;
                }

            case (AnchorPresets.HorStretchTop):
                {
                    source.anchorMin = new Vector2(0, 1);
                    source.anchorMax = new Vector2(1, 1);
                    break;
                }
            case (AnchorPresets.HorStretchMiddle):
                {
                    source.anchorMin = new Vector2(0, 0.5f);
                    source.anchorMax = new Vector2(1, 0.5f);
                    break;
                }
            case (AnchorPresets.HorStretchBottom):
                {
                    source.anchorMin = new Vector2(0, 0);
                    source.anchorMax = new Vector2(1, 0);
                    break;
                }

            case (AnchorPresets.VertStretchLeft):
                {
                    source.anchorMin = new Vector2(0, 0);
                    source.anchorMax = new Vector2(0, 1);
                    break;
                }
            case (AnchorPresets.VertStretchCenter):
                {
                    source.anchorMin = new Vector2(0.5f, 0);
                    source.anchorMax = new Vector2(0.5f, 1);
                    break;
                }
            case (AnchorPresets.VertStretchRight):
                {
                    source.anchorMin = new Vector2(1, 0);
                    source.anchorMax = new Vector2(1, 1);
                    break;
                }

            case (AnchorPresets.StretchAll):
                {
                    source.anchorMin = new Vector2(0, 0);
                    source.anchorMax = new Vector2(1, 1);
                    break;
                }
        }
    }

    public static void SetPivot(this RectTransform source, PivotPresets preset)
    {

        switch (preset)
        {
            case (PivotPresets.TopLeft):
                {
                    source.pivot = new Vector2(0, 1);
                    break;
                }
            case (PivotPresets.TopCenter):
                {
                    source.pivot = new Vector2(0.5f, 1);
                    break;
                }
            case (PivotPresets.TopRight):
                {
                    source.pivot = new Vector2(1, 1);
                    break;
                }

            case (PivotPresets.MiddleLeft):
                {
                    source.pivot = new Vector2(0, 0.5f);
                    break;
                }
            case (PivotPresets.MiddleCenter):
                {
                    source.pivot = new Vector2(0.5f, 0.5f);
                    break;
                }
            case (PivotPresets.MiddleRight):
                {
                    source.pivot = new Vector2(1, 0.5f);
                    break;
                }

            case (PivotPresets.BottomLeft):
                {
                    source.pivot = new Vector2(0, 0);
                    break;
                }
            case (PivotPresets.BottomCenter):
                {
                    source.pivot = new Vector2(0.5f, 0);
                    break;
                }
            case (PivotPresets.BottomRight):
                {
                    source.pivot = new Vector2(1, 0);
                    break;
                }
        }
    }
}
#endregion