using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using TMPro;

public class UtilityMessagePanel : MonoBehaviour
{
    #region PUBLIC_VARIABLES

    public TextMeshProUGUI txtTitle;

    [Header("Buttons")]
    public Button btnAffirmativeAction;
    public Button btnNegativeAction;
    public Button btnOK;

    [Header("Button Texts")]
    public Text txtAffirmativeButton;
    public Text txtNegativeButton;
    public Text txtOKButton;
    public Image MainImage;
    public Sprite[] Options;
    #endregion

    #region PRIVATE_VARIABLES

    #endregion

    #region UNITY_CALLBACKS

    // Use this for initialization
    void OnEnable()
    {
        transform.SetAsLastSibling();
        /*if (UIManager.Instance.LoginRegisterScreen.gameObject.activeInHierarchy)
        {
            transform.localScale = new Vector2(2f, 2f);
        }
        else
        {
            transform.localScale = new Vector2(1f, 1f);

        }*/
        transform.localScale = new Vector2(1f, 1f);

    }

    #endregion

    #region DELEGATE_CALLBACKS

    #endregion

    #region PUBLIC_METHODS

    public void OnCloseButtonTap()
    {
        gameObject.SetActive(false);
    }

    #endregion

    #region PRIVATE_METHODS

    #endregion

    #region COROUTINES

    #endregion
}