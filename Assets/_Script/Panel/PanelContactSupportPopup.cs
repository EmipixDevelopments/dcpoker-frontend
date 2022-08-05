using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;

public class PanelContactSupportPopup : MonoBehaviour
{
    [Header("Panel1")]
    [SerializeField] private GameObject _panel1;
    [SerializeField] private TMP_InputField _messageInputFieldPanel1;
    [SerializeField] private TextMeshProUGUI _charCounterTextPanel1;
    [SerializeField] private Button _closeButtonPanel1;
    [SerializeField] private Button _sendButtonPanel1;
    [Header("Panel2")]
    [SerializeField] private GameObject _panel2;
    [SerializeField] private TextMeshProUGUI _infoTextPanel2;
    [SerializeField] private Button _closeButtpnPanel2;

    private enum State { Panel1, Panel2 }

    void Start()
    {
        // init panel 1
        _closeButtonPanel1.onClick.RemoveAllListeners();
        _sendButtonPanel1.onClick.RemoveAllListeners();
        _closeButtonPanel1.onClick.AddListener(OnClickCloseButton);
        _sendButtonPanel1.onClick.AddListener(OnClickSendButton);
        _messageInputFieldPanel1.onValueChanged.RemoveAllListeners();
        _messageInputFieldPanel1.onValueChanged.AddListener(InputFieldValueChanged);
        // init panel 2
        _closeButtpnPanel2.onClick.RemoveAllListeners();
        _closeButtpnPanel2.onClick.AddListener(OnClickCloseButton);
    }

    private void OnEnable()
    {
        ChanheState(State.Panel1);
    }

    private void ChanheState(State newState)
    {
        CloseAllPanels();

        switch (newState)
        {
            case State.Panel1:
                OpenPanel1();
                break;
            case State.Panel2:
                OpenPanel2();
                break;
            default:
                break;
        }
    }

    private void CloseAllPanels()
    {
        _panel1.SetActive(false);
        _panel2.SetActive(false);
    }

    private void OpenPanel1()
    {
        _panel1.SetActive(true);
        _messageInputFieldPanel1.text = "";
    }
    private void OpenPanel2()
    {
        _panel2.SetActive(true);
        string username = UIManager.Instance.assetOfGame.SavedLoginData.Username;
        _infoTextPanel2.text =
            $"Thank you <b>{username}</b>\n" +
            $"your message has been sent";
    }


    private void InputFieldValueChanged(string arg0)
    {
        _charCounterTextPanel1.text = $"{_messageInputFieldPanel1.text.Length}/{_messageInputFieldPanel1.characterLimit}";
    }

    private void OnClickSendButton()
    {
        Debug.Log($"Sent to report: {_messageInputFieldPanel1.text}");
        ChanheState(State.Panel2);
    }

    private void OnClickCloseButton()
    {
        gameObject.SetActive(false);
    }
}
