using System;
using System.Collections;
using System.Collections.Generic;
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
