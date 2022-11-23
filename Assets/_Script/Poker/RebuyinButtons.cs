using UnityEngine;
using UnityEngine.UI;

public class RebuyinButtons : MonoBehaviour
{
    [SerializeField] private Button _addMoneyButton;
    [SerializeField] private Button _addChipsButton;

    private GamePanel _gamePanel;

    public void Init(GamePanel gamePanel)
    {
        _gamePanel = gamePanel;
        _addChipsButton.onClick.AddListener(_gamePanel.GetPlayerReBuyInChips);
        _addChipsButton.onClick.AddListener(_gamePanel.GetPlayerReBuyInChips); //todo goto money
    }

    public void OnDestroy()
    {
        _addChipsButton.onClick.RemoveListener(_gamePanel.GetPlayerReBuyInChips);
        _addChipsButton.onClick.RemoveListener(_gamePanel.GetPlayerReBuyInChips); // goto money
    }

    private void ResetButton()
    {
        _addMoneyButton.gameObject.SetActive(false);
        _addChipsButton.gameObject.SetActive(false);
    }

    public void OnEnable()
    {
        ResetButton();
        
        if (_gamePanel.IsCash())
            _addMoneyButton.gameObject.SetActive(true);
        else
            _addChipsButton.gameObject.SetActive(true);
    }
}
