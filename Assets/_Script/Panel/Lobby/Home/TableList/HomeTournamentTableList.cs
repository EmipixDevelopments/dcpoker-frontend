using UnityEngine;

public class HomeTournamentTableList : MonoBehaviour
{
    [SerializeField] private HomeBigTableElement _prefab;
    private int _amountElements = 5;

    private void Start()
    {
        Init();
    }

    private void Init()
    {
        for (var i = 0; i < _amountElements; i++)
        {
            Instantiate(_prefab, transform);
        }
    }
    public void UpdateList()
    {
        
    }
}
