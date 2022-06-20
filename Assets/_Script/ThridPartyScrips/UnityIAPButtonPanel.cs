using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Purchasing;

public class UnityIAPButtonPanel : MonoBehaviour
{
    #region PUBLIC_VARIABLES
    #endregion

    #region PRIVATE_VARIABLES
    #endregion

    #region UNITY_CALLBACKS
    // Start is called before the first frame update
    void Start()
    {

    }

    void Update()
    {

    }
    #endregion

    #region DELEGATE_CALLBACKS
    #endregion

    #region PUBLIC_METHODS
    public void Purchase10k(Product product)
    {
        //Utility.Instance.IAPCall(product, product.metadata.localizedPrice, 10000);
    }

    public void Purchase50k(Product product)
    {
        //Utility.Instance.IAPCall(product, product.metadata.localizedPrice, 50000);
    }

    public void Purchase100k(Product product)
    {
        //Utility.Instance.IAPCall(product, product.metadata.localizedPrice, 100000);
    }
    #endregion

    #region PRIVATE_METHODS
    #endregion

    #region COROUTINES
    #endregion

    #region GETTER_SETTER
    #endregion
}
