using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PanelTransferToOtherPlayerPopup : MonoBehaviour
{
    [SerializeField] private TMP_InputField _playerNameInputField;
    [SerializeField] private Image _ammountImage;
    [SerializeField] private TMP_InputField _ammountInputField;
    [SerializeField] private TextMeshProUGUI _ballanceText;
    [SerializeField] private GameObject _errorInfo;
    [SerializeField] private Button _cancelButton;
    [SerializeField] private Button _confirmButton;
    [Space]
    [SerializeField] private Sprite _cashSprite;
    [SerializeField] private Sprite _chipSprite;

    private void Start()
    {
        _cancelButton.onClick.RemoveAllListeners();
        _cancelButton.onClick.AddListener(() => gameObject.SetActive(false));

        _confirmButton.onClick.RemoveAllListeners();
        _confirmButton.onClick.AddListener(OnClickConfirmButtonFirstPanel);

        _ammountInputField.onValueChanged.RemoveAllListeners();
        _ammountInputField.onValueChanged.AddListener(OnValueChangedAmmountInputField);
    }

    private void OnEnable()
    {
        _playerNameInputField.text = "";
        _ammountInputField.text = "";
        _errorInfo.SetActive(false);
        if (UIManager.Instance)
        {
            if (UIManager.Instance.assetOfGame.SavedLoginData.isCash)
            {
                _ammountImage.sprite = _cashSprite;
                _ballanceText.text = $"your ballance: {UIManager.Instance.assetOfGame.SavedLoginData.cash}".ToUpper();
            }
            else
            {
                _ammountImage.sprite = _chipSprite;
                _ballanceText.text = $"your ballance: {UIManager.Instance.assetOfGame.SavedLoginData.chips}".ToUpper();
            }
        }
    }

    private void OnClickConfirmButtonFirstPanel()
    {
        string userName = _playerNameInputField.text;
        double ammount = 0;
        double.TryParse(_ammountInputField.text, out ammount);
        string padssword = UIManager.Instance.assetOfGame.SavedLoginData.password;

        if (UIManager.Instance.assetOfGame.SavedLoginData.isCash)
        {
            UIManager.Instance.SocketGameManager.TransferCash(userName, ammount, padssword, (socket, packet, args) =>
            {
                Debug.Log("TransferChips response  : " + packet.ToString());
                UIManager.Instance.HideLoader();

                JSONArray arr = new JSONArray(packet.ToString());
                string Source = arr.getString(arr.length() - 1);

                PokerEventResponse resp = JsonUtility.FromJson<PokerEventResponse>(Source);

                if (resp.status.Equals(Constants.PokerAPI.KeyStatusSuccess))
                {
                    UIManager.Instance.backgroundEventManager.GetProfileEventCall();
                }
                UIManager.Instance.DisplayMessagePanel(resp.message);
                this.Close();
            });
        }
        else
        {
            UIManager.Instance.SocketGameManager.TransferChips(userName, ammount, padssword, (socket, packet, args) =>
            {
                Debug.Log("TransferChips response  : " + packet.ToString());
                UIManager.Instance.HideLoader();

                JSONArray arr = new JSONArray(packet.ToString());
                string Source;
                Source = arr.getString(arr.length() - 1);
                var resp1 = Source;

                PokerEventResponse resp = JsonUtility.FromJson<PokerEventResponse>(resp1);

                if (resp.status.Equals(Constants.PokerAPI.KeyStatusSuccess))
                {
                    UIManager.Instance.backgroundEventManager.GetProfileEventCall();
                }
                UIManager.Instance.DisplayMessagePanel(resp.message);
                this.Close();
            });
        }
    }


    private void OnValueChangedAmmountInputField(string arg0)
    {
        double ammount = 0;
        double.TryParse(_ammountInputField.text, out ammount);

        if (UIManager.Instance.assetOfGame.SavedLoginData.isCash)
        {
            if (UIManager.Instance.assetOfGame.SavedLoginData.cash < ammount)
            {
                _errorInfo.SetActive(true);
            }
            else
            {
                _errorInfo.SetActive(false);
            }
        }
        else
        {
            if (UIManager.Instance.assetOfGame.SavedLoginData.chips < ammount)
            {
                _errorInfo.SetActive(true);
            }
            else
            {
                _errorInfo.SetActive(false);
            }
        }
    }
}