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
    [SerializeField] private MessageBubble _messageBubble;
    [SerializeField] private Transform _infoListTransform;
    
    private List<MessageBubble> _messageBubbles;
    private List<string> _massagesReadId;
    private bool _isNeedMessageUpdate = true;
    private RectTransform _rectTransform;

    private void Start()
    {
        _rectTransform = gameObject.GetComponent<RectTransform>();
        _messageBubbles = new List<MessageBubble>();
        _massagesReadId = new List<string>();
        UpdateMessages();
        
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
        if (UIManager.Instance)
        {
            CallProfileEvent();

            if (_isNeedMessageUpdate)
            {
                UpdateMessages();
            }
            else
            {
                _isNeedMessageUpdate = true;
            }
        }
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

    public void UpdateMessages()
    {
        var uiManager = UIManager.Instance;
        if(_messageBubbles == null)
            return;

        var messages = uiManager.LobbyPanelNew.Messages;
        messages.CheckMessage();
        
        var messagesDetails = messages.GetMessagesDetails();
        var amount = 0;
        
        foreach (var result in messagesDetails.result)
        {
            if (result.read || _massagesReadId.Contains(result._id)) 
                continue;
            
            if (_messageBubbles.Count - 1 < amount)
                CreateNewBubble();

            _messageBubbles[amount].SetData(result);
            _messageBubbles[amount].gameObject.SetActive(true);
            
            amount++;
        }
        foreach (var messageBubble in _messageBubbles)
        {
            var equalMessage = _messageBubbles.Find(result => result.IsEqual(messageBubble) && result != messageBubble);
            if (equalMessage != null)
            {
                equalMessage.SetData(null);
                equalMessage.gameObject.SetActive(false);
            }

            if (_massagesReadId.Contains(messageBubble.GetMessageId()) || messageBubble.IsRead())
            {
                messageBubble.gameObject.SetActive(false);
            }
        }
        
        //update ui 
        _isNeedMessageUpdate = false;
        LayoutRebuilder.ForceRebuildLayoutImmediate(_rectTransform);
        uiManager.LobbyPanelNew.UpdatePanel();
    }

    public void AddMessageToRead(string _id)
    {
        _massagesReadId.Add(_id);
        var messages = UIManager.Instance.LobbyPanelNew.Messages;
        messages.SetMessagesReadId(_massagesReadId);
    }
    
    private void CreateNewBubble()
    {
        var messageBubble = Instantiate(_messageBubble, _infoListTransform);
        messageBubble.Init(this);
        _messageBubbles.Add(messageBubble);
    }
}
