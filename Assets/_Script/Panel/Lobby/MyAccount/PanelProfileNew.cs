using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;
using System.Linq;
using Object = UnityEngine.Object;

public class PanelProfileNew : MonoBehaviour
{
    [SerializeField] private Button _changeNameButton;
    [SerializeField] private Button _changePasswordButton;
    [SerializeField] private Button _deleteAccountButton;
    [SerializeField] private Image _avatarImage;
    [SerializeField] private TextMeshProUGUI _userName;
    [SerializeField] private TextMeshProUGUI _chipsValue;
    [SerializeField] private TextMeshProUGUI _cashValue;
    [SerializeField] private TextMeshProUGUI _phoneNumber;
    [SerializeField] private Toggle _chipsToggle;
    [SerializeField] private Toggle _cashToggle;
    [Space]
    [SerializeField] private GameObject _panelChangeName;
    [SerializeField] private GameObject _panelChangePassword;
    [SerializeField] private GameObject _panelInformationAboutMoneyOnAccountPopup;
    [Space] 
    [SerializeField] private MessageBubble _messageBubbleElement;
    [SerializeField] private Transform _infoListTransform;
    
    private TableContainer<MessageBubble> _messageBubbleTableContainer;
    private RectTransform _rectTransform;

    private Messages _messages;
    private List<MessageData> _messagesData;

    private void Awake()
    {
        _rectTransform = gameObject.GetComponent<RectTransform>();
        
        _messageBubbleTableContainer = new TableContainer<MessageBubble>(_infoListTransform,_messageBubbleElement, 
            element => element.Init(this), true);
    }

    private void Start()
    {
        _changeNameButton.onClick.RemoveAllListeners();
        _changePasswordButton.onClick.RemoveAllListeners();
        _deleteAccountButton.onClick.RemoveAllListeners();
        _cashToggle.onValueChanged.RemoveAllListeners();

        _changeNameButton.onClick.AddListener(()=> _panelChangeName.SetActive(true));
        _changePasswordButton.onClick.AddListener(() => _panelChangePassword.SetActive(true));
        _deleteAccountButton.onClick.AddListener(() => _panelInformationAboutMoneyOnAccountPopup.SetActive(true));
        _cashToggle.onValueChanged.AddListener(OnClickChipsOrCashs);
    }

    void OnEnable()
    {
        var uiManager = UIManager.Instance;
        if(uiManager == null)
            return;
        
        CallProfileEvent();
        
        _messages = uiManager.LobbyPanelNew.Messages;
        _messages.AddUpdateListener(out _messagesData, UpdateMessages);
        UpdateMessages();
    }

    private void OnDisable()
    {
        var uiManager = UIManager.Instance;
        if(uiManager == null)
            return;

        uiManager.LobbyPanelNew.Messages.RemoveUpdateListener(UpdateMessages);
    }


    private void FixedUpdate()
    {
        // autoupdate
        UpdateFields();
    }


    public void CallProfileEvent()
    {
        UIManager.Instance.SocketGameManager.GetProfile((socket, packet, args) =>
        {
            Debug.Log("GetProfile  : " + packet.ToString());

            UIManager.Instance.HideLoader();
            JSONArray arr = new JSONArray(packet.ToString());
            string Source;
            Source = arr.getString(arr.length() - 1);
            var resp1 = Source;

            PokerEventResponse<PlayerLoginResponse> resp = JsonUtility.FromJson<PokerEventResponse<PlayerLoginResponse>>(resp1);

            if (resp.status.Equals(Constants.PokerAPI.KeyStatusSuccess))
            {
                UIManager.Instance.assetOfGame.SavedLoginData.SelectedAvatar = resp.result.profilePic;
                UIManager.Instance.assetOfGame.SavedLoginData.chips = resp.result.chips;
                UIManager.Instance.assetOfGame.SavedLoginData.cash = resp.result.cash;
                UIManager.Instance.assetOfGame.SavedLoginData.Username = resp.result.username;
                UIManager.Instance.assetOfGame.SavedLoginData.PlayerId = resp.result.playerId;
                UpdateFields();

                if (resp.result.isCash)
                {
                    _cashToggle.isOn = true;
                }
                else
                {
                    _chipsToggle.isOn = true;
                }
            }
            else
            {
                UIManager.Instance.DisplayMessagePanel(resp.message);
            }
        });
    }

    public void OnClickChipsOrCashs(bool isCashe) 
    {
        UIManager.Instance.SocketGameManager.UpdateIsCashe(isCashe, (socket, packet, args) =>
        {
            UIManager.Instance.HideLoader();
            JSONArray arr = new JSONArray(packet.ToString());
            string Source = arr.getString(arr.length() - 1);

            PokerEventResponse resp = JsonUtility.FromJson<PokerEventResponse>(Source);
            Debug.Log("UpdateIsCashe Response  : " + Source);
            if (resp.status.Equals(Constants.PokerAPI.KeyStatusSuccess))
            {
                UIManager.Instance.assetOfGame.SavedLoginData.isCash = isCashe;
            }
            else
            {
                UIManager.Instance.DisplayMessagePanel(resp.message);
            }
        });
    }

    private void UpdateFields()
    {
        _avatarImage.sprite = UIManager.Instance.assetOfGame.profileAvatarList.profileAvatarSprite[UIManager.Instance.assetOfGame.SavedLoginData.SelectedAvatar];
        _chipsValue.text = UIManager.Instance.assetOfGame.SavedLoginData.chips.ToString();
        _cashValue.text = UIManager.Instance.assetOfGame.SavedLoginData.cash.ToString();
        _phoneNumber.text = $"{UIManager.Instance.assetOfGame.SavedLoginData.phoneCode} {UIManager.Instance.assetOfGame.SavedLoginData.phoneNumber}";
        _userName.text = UIManager.Instance.assetOfGame.SavedLoginData.Username;
    }

    private void UpdateMessages()
    {
        for (var i = 0; i < _messagesData.Count; i++)
        {
            _messageBubbleTableContainer.GetElement(i).SetData(_messagesData[i]);
        }
        _messageBubbleTableContainer.HideFromIndex(_messagesData.Count);
        
        LayoutRebuilder.ForceRebuildLayoutImmediate(_rectTransform);
        LayoutRebuilder.ForceRebuildLayoutImmediate(GetComponent<RectTransform>());
        UIManager.Instance.LobbyPanelNew.UpdateUi();

    }
    
    public void AddMessageToRead(MessageData messageData)
    {
        _messages.AddReedMessage(messageData);
    }
}
