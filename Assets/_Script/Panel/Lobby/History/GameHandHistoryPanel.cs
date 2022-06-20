using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameHandHistoryPanel : MonoBehaviour
{
    #region PUBLIC_VARIABLES
    public HistoryRowObject historyObjectPrefab;
    public Transform transParentContainer;
    public ScrollRect myScrollRect;
    public PaginationPanel paginationPanel;
    public Color color1;
    public Color color2;
    #endregion

    #region PRIVATE_VARIABLES
    public List<HistoryRowObject> historyObjectList = new List<HistoryRowObject>();
    #endregion

    #region UNITY_CALLBACK
    void OnEnable()
    {
        UIManager.Instance.SoundManager.OnButtonClick();
        paginationPanel.Close();
        myScrollRect.verticalNormalizedPosition = 1f;
        CallGameHandHistoryEvent(1);
    }

    void OnDisable()
    {
        ResetPanel();
    }
    #endregion

    #region DELEGATE_CALLBACKS
    #endregion

    #region PUBLIC_METHODS

    //Poker king mobile method
    public void OnHistoryBackButtonTap()
    {
        this.gameObject.SetActive(false);
    }

    public void PaginationUpdateCall()
    {
        ResetPanel();
        CallGameHandHistoryEvent(paginationPanel.selectedPage);
    }
    #endregion

    #region PRIVATE_METHODS

    private void ResetPanel()
    {
        foreach (HistoryRowObject historyObject in historyObjectList)
        {
            Destroy(historyObject.gameObject);
        }
        historyObjectList.Clear();
    }

    private void CallGameHandHistoryEvent(int pageNo)
    {
        UIManager.Instance.DisplayLoader("");
        UIManager.Instance.SocketGameManager.GameHistoryList(pageNo, (socket, packet, args) =>
            {
                print("GameHistoryList  : " + packet.ToString());

                UIManager.Instance.HideLoader();
                JSONArray arr = new JSONArray(packet.ToString());
                PokerEventResponse<GameHandHistoryResult> gameHandHistoryResponse = JsonUtility.FromJson<PokerEventResponse<GameHandHistoryResult>>(arr.getString(0));
                if (gameHandHistoryResponse.status.Equals(Constants.PokerAPI.KeyStatusSuccess))
                {
                    int count = 1;
                    foreach (GameHandHistoryResult.GameHistoryList history in gameHandHistoryResponse.result.gamesHistoryList)
                    {
                        HistoryRowObject historyObject = Instantiate(historyObjectPrefab, transParentContainer).GetComponent<HistoryRowObject>();
                        Color imgColor = count++ % 2 != 0 ? color1 : color2;
                        historyObject.SetData(history, imgColor);
                        historyObject.Open();

                        historyObjectList.Add(historyObject);
                    }

                    paginationPanel.SetData(gameHandHistoryResponse.result.currentPage, gameHandHistoryResponse.result.recordsTotal, gameHandHistoryResponse.result.resultPerPage);
                }
                else
                {
                    UIManager.Instance.DisplayMessagePanel(gameHandHistoryResponse.message, null);
                }
            });
    }

    #endregion

    #region COROUTINES

    #endregion
}
