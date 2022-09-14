using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;

public class DetailsTournamentNewLeftPanel : MonoBehaviour
{
    [SerializeField] private DetailsTournamentNew _detailsTournamentNew;

    [Header("Toggles")]
    [SerializeField] private Toggle _playersToggle;
    [SerializeField] private Toggle _tablesToggle;
    [SerializeField] private Toggle _playoutsToggle;
    [SerializeField] private Toggle _blindsToggle;

    [Header("Panels")]
    [SerializeField] private PlayerDetailsTorunament _playersPanel;
    [SerializeField] private TableDetails _tablesPanel;
    [SerializeField] private payoutDetails _playoutsPanel;
    [SerializeField] private BlindDetails _blindsPanel;
    
    private Animator _animator;
    private int _openAnimationId = Animator.StringToHash("open");
    private int _closeAnimationId = Animator.StringToHash("close");

    private void Awake()
    {
        _animator = GetComponent<Animator>();
    }

    private void OnEnable()
    {
        _animator.Play(_openAnimationId);
    }

    public void SetActive(bool active)
    {
        if (active)
        {
            gameObject.SetActive(true);
        }
        else
        {
            if(gameObject.activeSelf)
                CloseAnim();
        }
    }
    private void CloseAnim()
    {
        _animator.Play(_closeAnimationId);
        DisableAfterAnimAsync();
    }

    private async void DisableAfterAnimAsync()
    {
        await Task.Delay(250); // This is animation duration
        gameObject.SetActive(false);
    }

    private void Start()
    {
        _playersToggle.onValueChanged.RemoveAllListeners();
        _tablesToggle.onValueChanged.RemoveAllListeners();
        _playoutsToggle.onValueChanged.RemoveAllListeners();
        _blindsToggle.onValueChanged.RemoveAllListeners();

        _playersToggle.onValueChanged.AddListener(OnChangePlayersToggle);
        _tablesToggle.onValueChanged.AddListener(OnCahangeTablesToggle);
        _playoutsToggle.onValueChanged.AddListener(OnCahangePlayoutsToggle);
        _blindsToggle.onValueChanged.AddListener(OnCahangeBlindsToggle);

        OnChangePlayersToggle(true);
    }
    private void OnChangePlayersToggle(bool arg0)
    {
        if (arg0 == false) return;
        CloseAll();
        _playersPanel.TournamentDetailsId = _detailsTournamentNew.TournamentDetailsId;
        _playersPanel.gameObject.SetActive(true);
    }
    private void OnCahangeTablesToggle(bool arg0)
    {
        if (arg0 == false) return;
        CloseAll();
        _tablesPanel.TournamentDetailsId = _detailsTournamentNew.TournamentDetailsId;
        _tablesPanel.gameObject.SetActive(true);
    }
    private void OnCahangePlayoutsToggle(bool arg0)
    {
        if (arg0 == false) return;
        CloseAll();
        _playoutsPanel.TournamentDetailsId = _detailsTournamentNew.TournamentDetailsId;
        _playoutsPanel.gameObject.SetActive(true);
    }
    private void OnCahangeBlindsToggle(bool arg0)
    {
        if (arg0 == false) return;
        CloseAll();
        _blindsPanel.TournamentDetailsId = _detailsTournamentNew.TournamentDetailsId;
        _blindsPanel.gameObject.SetActive(true);
    }

    private void CloseAll() 
    {
        _playersPanel.gameObject.SetActive(false);
        _tablesPanel.gameObject.SetActive(false);
        _playoutsPanel.gameObject.SetActive(false);
        _blindsPanel.gameObject.SetActive(false);
    }
}
