using System;
using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Text;
using UnityEngine;
using UnityEngine.UI;

public class TableManager : MonoBehaviour
{

    #region PUBLIC_VARIABLES
    [Header("List")]
    public List<string> playingTableList;
    public List<MiniTable> MiniTables;

    [Header("Prefabs")]
    public MiniTable miniTablePrefab;

    [Header("Transform")]
    public Transform transformMiniTableContainer;

    [Header("RectTransform")]
    public RectTransform rectTransformMiniTable;

    [Header("GameObject")]
    public GameObject gameObjectMultiTablePanel;

    [Header("Button")]
    public Button buttonAddTable;

    [Header("Integer")]
    public int maxTableLimit = 100;

    [Header("Sprite")]
    public Sprite cashMiniTable;
    public Sprite regularTournamentMiniTable;
    public Sprite sngTournamentMiniTable;
    #endregion

    #region PRIVATE_VARIABLES
    #endregion

    #region UNITY_CALLBACKS
    #endregion

    #region DELEGATE_CALLBACKS
    #endregion

    #region PUBLIC_METHODS
    public void AddMiniTable(RoomsListing.Room data)
    {
        print("!gameObjectMultiTablePanel.activeSelf: " + !gameObjectMultiTablePanel.activeSelf);
        if (!gameObjectMultiTablePanel.activeSelf)
        {
            gameObjectMultiTablePanel.SetActive(true);
        }

        UIManager.Instance.tableManager.playingTableList.Add(data.roomId);

        MiniTable miniTable = Instantiate(miniTablePrefab, transformMiniTableContainer);
        miniTable.SetMiniTableData(data);
        miniTable.imgSelected.SetActive(false);
        MiniTables.Add(miniTable);

        if (data.isTournament)
        {
            miniTable.imgTable.sprite = regularTournamentMiniTable;
        }
        else
        {
            miniTable.imgTable.sprite = cashMiniTable;
        }
        buttonAddTable.transform.SetAsLastSibling();
    }

    public void HighlightMiniTable(string roomId)
    {
        foreach (MiniTable miniTable in MiniTables)
        {
            if (miniTable.roomId == roomId)
            {
                miniTable.imgSelected.SetActive(true);
                break;
            }
        }
    }

    public void RemoveMiniTable(string roomId)
    {
        foreach (MiniTable miniTable in MiniTables)
        {
            if (miniTable.roomId == roomId)
            {
                Destroy(miniTable.gameObject);
                MiniTables.Remove(miniTable);
                playingTableList.Remove(roomId);
                break;
            }
        }
    }

    public void ReplaceMiniTableRoomId(string roomId, string newRoomId)
    {
        for (int i = 0; i < playingTableList.Count; i++)
        {
            if (playingTableList[i] == roomId)
            {
                playingTableList[i] = newRoomId;
                break;
            }
        }

        foreach (MiniTable miniTable in MiniTables)
        {
            if (miniTable.miniTableRoomData.roomId == roomId)
                miniTable.miniTableRoomData.roomId = newRoomId;
        }
    }

    public bool IsMiniTableExists(string roomId)
    {
        foreach (MiniTable miniTable in MiniTables)
        {
            if (miniTable.miniTableRoomData.roomId == roomId)
                return true;
        }

        return false;
    }

    public bool IsMiniTableTournamentExisted(string tournamentId)
    {
        foreach (MiniTable miniTable in MiniTables)
        {
            if (miniTable.miniTableRoomData.isTournament && miniTable.miniTableRoomData.tournamentId == tournamentId)
                return true;
        }

        return false;
    }

    public void ReSubscribeMiniTables(string roomId)
    {
        foreach (MiniTable miniTable in MiniTables)
        {
            if (miniTable.miniTableRoomData.roomId == roomId)
                miniTable.ReSubscribeRoom();
        }
    }

    public void ReSubscribeMiniTables()
    {
        foreach (MiniTable miniTable in MiniTables)
        {
            miniTable.ReSubscribeRoom();
        }
    }

    public void TableSelection(string roomId)
    {
        foreach (MiniTable miniTable in MiniTables)
        {
            if (miniTable.miniTableRoomData.roomId == roomId)
                miniTable.imgSelected.SetActive(true);
        }
    }

    public void DeselectAllTableSelection()
    {
        foreach (MiniTable miniTable in MiniTables)
        {
            miniTable.imgSelected.SetActive(false);
        }
    }

    public void RemoveAllMiniTableData()
    {
        foreach (MiniTable miniTable in MiniTables)
        {
            Destroy(miniTable.gameObject);
        }
        MiniTables.Clear();
        playingTableList.Clear();
    }

    public void MiniTablePosition(int xPosition)
    {
        RectTransformExtensions.SetAnchor(UIManager.Instance.tableManager.rectTransformMiniTable, AnchorPresets.TopLeft, xPosition, -20);
    }

    public void ShowAddTableButton()
    {
        if (MiniTables.Count < maxTableLimit)
        {
            buttonAddTable.Open();
        }
        else
        {
            buttonAddTable.Close();
        }
    }

    public void HideAddTableButton()
    {
        buttonAddTable.Close();
    }

    public MiniTable GetMiniTable(string roomId)
    {
        foreach (MiniTable minitable in MiniTables)
        {
            if (minitable.roomId == roomId)
                return minitable;
        }

        return null;
    }
    #endregion

    #region PRIVATE_METHODS
    private MiniTable GetMinitableByRoomId(string roomId)
    {
        foreach (MiniTable miniTable in MiniTables)
        {
            if (miniTable.roomId == roomId)
            {
                return miniTable;
            }
        }

        return null;
    }
    #endregion

    #region COROUTINES
    bool touchLock = false;
    public IEnumerator SwitchGameTable(RoomsListing.Room miniTableRoomData)
    {
        if (touchLock == false)
        {
            touchLock = true;

            foreach (MiniTable tables in MiniTables)
            {
                tables.imgSelected.SetActive(false);
            }
            UIManager.Instance.DisplayLoader("");
            UIManager.Instance.GameScreeen.Close();

            if (UIManager.Instance.LobbyScreeen.isActiveAndEnabled)
            {
                yield return new WaitForSeconds(0f);
            }
            else
            {
                yield return new WaitForSeconds(0.1f);
            }

            Constants.Poker.TableId = miniTableRoomData.roomId;
            UIManager.Instance.GameScreeen.currentRoomData = new RoomsListing.Room();
            foreach (MiniTable tables in MiniTables)
            {
                if (miniTableRoomData.roomId.Equals(tables.roomId))
                {
                    tables.imgSelected.SetActive(true);
                }
                else
                {
                    tables.imgSelected.SetActive(false);
                }
            }
            UIManager.Instance.GameScreeen.SetRoomDataAndPlay(miniTableRoomData);
            touchLock = false;

            if (UIManager.Instance.LobbyScreeen.isActiveAndEnabled)
                yield return new WaitForSeconds(0f);
            else
                yield return new WaitForSeconds(0.1f);

            if (!UIManager.Instance.GameScreeen.isActiveAndEnabled)
            {
                UIManager.Instance.GameScreeen.Open();
                UIManager.Instance.LobbyScreeen.Close();
            }
            yield return 0;

            DeselectAllTableSelection();
            MiniTable miniTable = GetMinitableByRoomId(miniTableRoomData.roomId);
            if (miniTable != null)
                miniTable.imgSelected.SetActive(true);

            yield return new WaitForSeconds(0.75f);
            touchLock = false;
        }
    }
    public string AESEncryption(string inputData)
    {
        AesCryptoServiceProvider AEScryptoProvider = new AesCryptoServiceProvider();
        AEScryptoProvider.BlockSize = 128;
        AEScryptoProvider.KeySize = 256;
        AEScryptoProvider.Key = ASCIIEncoding.ASCII.GetBytes("pZpK2nWyxj9mvqyEyWRETGqTAmLYmbyX");
        AEScryptoProvider.IV = ASCIIEncoding.ASCII.GetBytes("RRfC77zAybcqTXNQ");
        AEScryptoProvider.Mode = CipherMode.CBC;
        AEScryptoProvider.Padding = PaddingMode.PKCS7;

        byte[] txtByteData = ASCIIEncoding.ASCII.GetBytes(inputData);
        ICryptoTransform trnsfrm = AEScryptoProvider.CreateEncryptor(AEScryptoProvider.Key, AEScryptoProvider.IV);

        byte[] result = trnsfrm.TransformFinalBlock(txtByteData, 0, txtByteData.Length);
        return Convert.ToBase64String(result);
    }
    #endregion

    #region GETTER_SETTER
    #endregion
}
