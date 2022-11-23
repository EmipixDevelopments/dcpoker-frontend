using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;


public class PanelHome : MonoBehaviour
{
    [SerializeField] private HomeTournamentTableList _homeTournamentTableList;
    [SerializeField] private HomeTexasHoldemTableList _homeTexasHoldemTableList;
    [SerializeField] private HomeOmahaPolo5TableList _homeOmahaPolo5TableList;
    [SerializeField] private Button _joinFreeTournamentButton;

    private const int DelayTimeMilliseconds = 8000;
    
    private bool _isNeedUpdateTable;

    private void Start()
    {
        _joinFreeTournamentButton.onClick.AddListener(OnJoinFreeTournamentButton);
    }

    private void OnDestroy()
    {
        _joinFreeTournamentButton.onClick.RemoveListener(OnJoinFreeTournamentButton);
    }

    private void OnEnable()
    {
        StartUpdateTableListsAsync();
    }

    private void OnDisable()
    {
        _isNeedUpdateTable = false;
    }

    private async void StartUpdateTableListsAsync()
    {
        _isNeedUpdateTable = true;
        
        while (_isNeedUpdateTable)
        {
            UpdateTableLists();
            await Task.Delay(DelayTimeMilliseconds);
        }
    }

    private void UpdateTableLists()
    {
        _homeTournamentTableList.UpdateList();
        _homeTexasHoldemTableList.UpdateList();
        _homeOmahaPolo5TableList.UpdateList();
    }

    private void OnJoinFreeTournamentButton()
    {
        
    }
}
