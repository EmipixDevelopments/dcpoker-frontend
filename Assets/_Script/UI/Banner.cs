using BestHTTP.SocketIO;
using Constants;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

public class Banner : MonoBehaviour
{
    [SerializeField] private Image _image;
    [SerializeField] private Button _button;
    [Space]
    [SerializeField] private BannerType _bannerType;
    
    private Action _onButtonClick;
    private bool _firstStart = false;
    private BannerDataRequest.BannerData _bannerData;

    private void Start()
    {
        _button.onClick.RemoveAllListeners();
        _button.onClick.AddListener(() => _onButtonClick?.Invoke()) ;
    }


    private void OnEnable()
    {
        if (!UIManager.Instance)
        {
            return;
        }
        UpdateBanner();
    }

    //private void FixedUpdate()
    //{
    //    // If the UI manager doesn't load before the banner
    //    if (!_firstStart && UIManager.Instance)
    //    {
    //        UpdateBanner();
    //    }
    //}

    private void UpdateBanner()
    {
        UIManager.Instance.SocketGameManager.Banner(_bannerType.ToString(), (socket, packet, args) =>
        {
            print("Banners: " + packet.ToString());
            JSONArray arr = new JSONArray(packet.ToString());
            var resp = arr.getString(arr.length() - 1);
            BannerDataRequest bannerDataRequest = JsonUtility.FromJson<BannerDataRequest>(resp);
            _bannerData = bannerDataRequest.result;
 
            if (_bannerData.position == "tournament") TournamentBannerLogic(_bannerData);
 
            string url = PokerAPI.BaseUrl + _bannerData.image;
            StartCoroutine(DownloadAndShowImage(url));
        });
    }

    IEnumerator DownloadAndShowImage(string mediaUrl)
    {
        Debug.Log("--- mediaUrl" + mediaUrl);
        UnityWebRequest request = UnityWebRequestTexture.GetTexture(mediaUrl);
        yield return request.SendWebRequest();
        if (request.isNetworkError || request.isHttpError)
            Debug.Log(request.error);
        else
        {
            Texture2D tex = ((DownloadHandlerTexture)request.downloadHandler).texture;
            Sprite sprite = Sprite.Create(tex, new Rect(0, 0, tex.width, tex.height), new Vector2(tex.width / 2, tex.height / 2));
            _image.overrideSprite = sprite;
        }
    }

    #region Tournament Banner Logic
    private void TournamentBannerLogic(BannerDataRequest.BannerData roomsResp)
    {
        if (roomsResp.tournamentId.Length > 0)
        {
            _onButtonClick = () => {

                GetTournamentsList();
            };
        }
    }

    private void GetTournamentsList()
    {
        string tournamentPokerType = "all";
        string selectedGameSpeed = UIManager.Instance.selectedGameSpeed.ToString();
        bool isLimitSelected = false;
        string gametype = "Touranment";
        string selectedLimitType = "all";
        string selectedStack = "all";
        string selectedPlayerPerTable = "all";

        UIManager.Instance.SocketGameManager.SearchTournamentLobby(
            tournamentPokerType,
            selectedGameSpeed,
            "",
            isLimitSelected,
            gametype,
            selectedLimitType,
            selectedStack,
            selectedPlayerPerTable,
            FindAndOpenTournament);
    }

    private void FindAndOpenTournament(Socket socket, Packet packet, object[] args)
    {
        JSONArray arr = new JSONArray(packet.ToString());
        string Source = arr.getString(arr.length() - 1);
        NormalTournamentDetails touramentsDetail = JsonUtility.FromJson<NormalTournamentDetails>(Source);

        foreach (var tournament in touramentsDetail.result)
        {
            if (tournament.tournamentId == _bannerData.tournamentId)
            {
                UIManager.Instance.SoundManager.OnButtonClick();
                UIManager.Instance.DetailsTournament.TournamentDetailsId = _bannerData.tournamentId;
                UIManager.Instance.DetailsTournament.GetDetailsTournamentButtonTap(_bannerData.tournamentId, tournament.pokerGameType);
            }
        }
    }
    #endregion
}
