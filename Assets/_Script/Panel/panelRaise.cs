using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class panelRaise : MonoBehaviour
{
    #region PUBLIC_VARIABLES
    public Text ResultText;
    #endregion

    #region PRIVATE_VARIABLES
    #endregion

    #region UNITY_CALLBACKS

    #endregion

    #region DELEGATE_CALLBACKS
    #endregion

    #region PUBLIC_METHODS
    public void Setdata(string name)
    {
        //UIManager.Instance.SoundManager.RaiseClickOnce();
        ResultText.text = name + " Has Raise";
        this.Open();
        StartCoroutine(closeScreen(4f));
    }

    #endregion

    #region PRIVATE_METHODS
    #endregion

    #region COROUTINES
    public IEnumerator closeScreen(float timer)
    {
        yield return new WaitForSeconds(timer);

        this.Close();

    }
    #endregion

    #region GETTER_SETTER
    #endregion
}
