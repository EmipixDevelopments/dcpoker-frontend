using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SplashPanel : MonoBehaviour
{

    #region PUBLIC_VARIABLES 
    //[Header ("Gamobjects")]

    //[Header ("Transforms")]


    //[Header ("ScriptableObjects")]


    //[Header ("DropDowns")]


    [Header("Images")]
    public Image Loader;
    public Slider LoaderNew;

    //[Header ("Text")]


    //[Header ("Prefabs")]

    //[Header ("Enums")]


    //[Header ("Variables")]

    #endregion 
    #region PRIVATE_VARIABLES 
    #endregion 
    #region UNITY_CALLBACKS     // Use this for initialization
    void OnEnable()
    {
        Loader.fillAmount = 0;
        LoaderNew.value = 0;
#if UNITY_WEBGL || UNITY_STANDALONE || UNITY_EDITOR
        gameObject.transform.localScale = new Vector2(1f, 1f);
#endif 
        // DisplayProgressLoader(true);
        StartCoroutine(NextScreen(2f));

    }
    void OnDisable()
    {
        Loader.fillAmount = 0;
        LoaderNew.value = 0;
    }
    // Update is called once per frame
    void Update()
    {

    }
    #endregion 
    #region DELEGATE_CALLBACKS 

    #endregion 
    #region PUBLIC_METHODS 


    #endregion 
    #region PRIVATE_METHODS 
    #endregion 
    #region COROUTINES 
    public void DisplayProgressLoader(bool iscache = false)
    {
        StopCoroutine("DisplayProgressCoroutine");
        StartCoroutine(DisplayProgressCoroutine(iscache));
    }


    private IEnumerator LoadProgressBar()
    {
        float i = 0;
        while (i < 1)
        {
            i += 0.1f;
            yield return new WaitForSeconds(0.1f);
            //float amt = Mathf.Lerp(Loader.fillAmount, UIManager.Instance.assetsLoadedPercentage, i);
            //Loader.fillAmount = i;
            LoaderNew.value = i;
            yield return new WaitForEndOfFrame();
        }
    }

    private IEnumerator DisplayProgressCoroutine(bool iscache)
    {
        float i = 0;
        yield return new WaitForSeconds(2f);

        if (iscache)
        {
            while (i < 1)
            {
                i += Time.deltaTime / 3;
                //float amt = Mathf.Lerp(Loader.fillAmount, UIManager.Instance.assetsLoadedPercentage, i);
                float amt = UIManager.Instance.assetsLoadedPercentage;
                Loader.fillAmount = amt;

                Loader.fillAmount = amt;

            }
        }
        else
        {
            float amt = UIManager.Instance.assetsLoadedPercentage;

            Loader.fillAmount = amt;
            //Debug.Log("loader => " + amt + " current time => " + DateTime.Now.ToString());
            Loader.fillAmount = amt;
        }
        //UIManager.Instance.assetsLoadedPercentage = (UIManager.Instance.downloadedIconsList.Count / UIManager.Instance.totalIconsToDownload) * 1;
        //Debug.Log("ovesh => " + (UIManager.Instance.assetsLoadedPercentage / UIManager.Instance.totalIconsToDownload) * 1f);
        Loader.fillAmount = (UIManager.Instance.assetsLoadedPercentage / UIManager.Instance.totalIconsToDownload) * 1f;
        //		yield return new WaitForSeconds (.1f);

        if (Loader.fillAmount >= 1)
        {
            StartCoroutine(NextScreen(1f));
            yield return 0;
        }
    }

    IEnumerator NextScreen(float timer)
    {

        yield return new WaitForSeconds(timer);
        UIManager.Instance.MainHomeScreen.Open();
        UIManager.Instance.splashScreen.Close();

    }


    #endregion 

    #region GETTER_SETTER 

    #endregion 


}
