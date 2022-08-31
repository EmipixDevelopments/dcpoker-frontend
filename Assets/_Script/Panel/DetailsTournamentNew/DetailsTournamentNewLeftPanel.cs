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
    [SerializeField] private GameObject _playersPanel;
    [SerializeField] private GameObject _tablesPanel;
    [SerializeField] private GameObject _playoutsPanel;
    [SerializeField] private GameObject _blindsPanel;

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
        _playersPanel.SetActive(true);
    }
    private void OnCahangeTablesToggle(bool arg0)
    {
        if (arg0 == false) return;
        CloseAll();
        _tablesPanel.SetActive(true);
    }
    private void OnCahangePlayoutsToggle(bool arg0)
    {
        if (arg0 == false) return;
        CloseAll();
        _playoutsPanel.SetActive(true);
    }
    private void OnCahangeBlindsToggle(bool arg0)
    {
        if (arg0 == false) return;
        CloseAll();
        _blindsPanel.SetActive(true);
    }

    private void CloseAll() 
    {
        _playersPanel.SetActive(false);
        _tablesPanel.SetActive(false);
        _playoutsPanel.SetActive(false);
        _blindsPanel.SetActive(false);
    }
}
