using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Purchasing;

// Placing the Purchaser class in the CompleteProject namespace allows it to interact with ScoreManager,
// one of the existing Survival Shooter scripts.


// Deriving the Purchaser class from IStoreListener enables it to receive messages from Unity Purchasing.
public class UnityIAPManager : MonoBehaviour, IStoreListener
{
    public static UnityIAPManager instance;

    [Header("Objects")]
    public Transform iapTabsParent;
    public GameObject iapTabPrefab;

    private List<GameObject> _spawnedIapTabs = new List<GameObject>();
    public List<IAPData> _iapDatas;
    private List<IAPTab> _spawnedIAPTabsScript = new List<IAPTab>();

    private static IStoreController m_StoreController;
    // The Unity Purchasing system.
    private static IExtensionProvider m_StoreExtensionProvider;
    // The store-specific Purchasing subsystems.

    void Start()
    {
        // If we haven't set up the Unity Purchasing reference
        if (m_StoreController == null)
        {
            // Begin to configure our connection to Purchasing
            InitializePurchasing();
        }
    }

    public void OpenInappPanel()
    {
        this.Open();
    }

    public void OnCloseButtonClicked()
    {
        this.Close();
    }

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(this);
        }
    }

    void OnEnable()
    {
        Debug.Log("In Enable....");
        InitializePurchasing();
        //	UIManager.Instance.DisplayLoader ("");
    }

    void OnDisable()
    {
        DestroyAllSpawnedTabs();

    }

    public void InitializePurchasing()
    {
        Debug.Log("In purchasing..... ");
        // If we have already connected to Purchasing ...
        if (IsInitialized())
        {
            Debug.Log("Is intialized....");
            // return if it is not dynamic IAP.
            // return;
        }

        Debug.Log("00000000000");
        // Create a builder, first passing in a suite of Unity provided stores.
        var builder = ConfigurationBuilder.Instance(StandardPurchasingModule.Instance());

        Debug.Log("1111111111111111");
        UIManager.Instance.DisplayLoader();
        UIManager.Instance.SocketGameManager.InAppPurchase((socket, packet, args) =>
        {
            Debug.Log("........ iap packet string " + packet.ToString());
            JSONArray arr = new JSONArray(packet.ToString());
            string Source;
            Source = arr.getString(arr.length() - 1);
            var resp1 = Source;

            PokerEventListResponse<IAPData> iapDataContainer = JsonUtility.FromJson<PokerEventListResponse<IAPData>>(resp1);
            _iapDatas = iapDataContainer.result;
            for (int i = 0; i < _iapDatas.Count; i++)
            {
                builder.AddProduct(_iapDatas[i].in_app_purchase_id, ProductType.Consumable);
                Debug.Log("1111111111111111---------");

            }

            if (_iapDatas.Count > 0)
            {
                UnityPurchasing.Initialize(this, builder);
                Debug.Log("22222222222222222---------");

            }
        });
    }

    private void InAppPurchaseEventCall()
    {

    }

    private bool IsInitialized()
    {
        // Only say we are initialized if both the Purchasing references are set.
        return m_StoreController != null && m_StoreExtensionProvider != null;
    }

    public void BuyProductID(string productId)
    {
        // If Purchasing has been initialized ...
        if (IsInitialized())
        {
            // ... look up the Product reference with the general product identifier and the Purchasing 
            // system's products collection.
            Product product = m_StoreController.products.WithID(productId);

            // If the look up found a product for this device's store and that product is ready to be sold ... 
            if (product != null && product.availableToPurchase)
            {
                Debug.Log(string.Format("Purchasing product asychronously: '{0}'", product.definition.id));
                // ... buy the product. Expect a response either through ProcessPurchase or OnPurchaseFailed 
                // asynchronously.
                m_StoreController.InitiatePurchase(product);
            }
            // Otherwise ...
            else
            {
                // ... report the product look-up failure situation  
                Debug.Log("BuyProductID: FAIL. Not purchasing product, either is not found or is not available for purchase");
            }
        }
        // Otherwise ...
        else
        {
            // ... report the fact Purchasing has not succeeded initializing yet. Consider waiting longer or 
            // retrying initiailization.
            Debug.Log("BuyProductID FAIL. Not initialized.");
        }
    }


    // Restore purchases previously made by this customer. Some platforms automatically restore purchases, like Google.
    // Apple currently requires explicit purchase restoration for IAP, conditionally displaying a password prompt.
    public void RestorePurchases()
    {
        // If Purchasing has not yet been set up ...
        if (!IsInitialized())
        {
            // ... report the situation and stop restoring. Consider either waiting longer, or retrying initialization.
            Debug.Log("RestorePurchases FAIL. Not initialized.");
            return;
        }

        // If we are running on an Apple device ... 
        if (Application.platform == RuntimePlatform.IPhonePlayer ||
                      Application.platform == RuntimePlatform.OSXPlayer)
        {
            // ... begin restoring purchases
            Debug.Log("RestorePurchases started ...");

            // Fetch the Apple store-specific subsystem.
            var apple = m_StoreExtensionProvider.GetExtension<IAppleExtensions>();
            // Begin the asynchronous process of restoring purchases. Expect a confirmation response in 
            // the Action<bool> below, and ProcessPurchase if there are previously purchased products to restore.
            apple.RestoreTransactions((result) =>
            {
                // The first phase of restoration. If no more responses are received on ProcessPurchase then 
                // no purchases are available to be restored.
                Debug.Log("RestorePurchases continuing: " + result + ". If no further messages, no purchases available to restore.");
            });
        }
        // Otherwise ...
        else
        {
            // We are not running on an Apple device. No work is necessary to restore purchases.
            Debug.Log("RestorePurchases FAIL. Not supported on this platform. Current = " + Application.platform);
        }
    }


    //
    // --- IStoreListener
    //

    public void OnInitialized(IStoreController controller, IExtensionProvider extensions)
    {
        UIManager.Instance.HideLoader();
        // Purchasing has succeeded initializing. Collect our Purchasing references.
        Debug.Log("OnInitialized: PASS");

        // Overall Purchasing system, configured with products for this application.
        m_StoreController = controller;
        // Store specific subsystem, for accessing device-specific store features.
        m_StoreExtensionProvider = extensions;
        InstantiateIapTabs();
    }


    public void OnInitializeFailed(InitializationFailureReason error)
    {
        // Purchasing set-up has not succeeded. Check error for reason. Consider sharing this reason with the user.
        Debug.Log("OnInitializeFailed InitializationFailureReason:" + error);
    }


    public PurchaseProcessingResult ProcessPurchase(PurchaseEventArgs args)
    {
        for (int i = 0; i < _spawnedIAPTabsScript.Count; i++)
        {
            if (string.Equals(_spawnedIAPTabsScript[i].iapData.in_app_purchase_id, args.purchasedProduct.definition.id))
            {
                _spawnedIAPTabsScript[i].OnPurchaseComplete(args.purchasedProduct);
            }
        }
        return PurchaseProcessingResult.Complete;
    }


    public void OnPurchaseFailed(Product product, PurchaseFailureReason failureReason)
    {
        // A product purchase attempt did not succeed. Check failureReason for more detail. Consider sharing 
        // this reason with the user to guide their troubleshooting actions.
        for (int i = 0; i < _spawnedIAPTabsScript.Count; i++)
        {
            if (string.Equals(_spawnedIAPTabsScript[i].iapData.in_app_purchase_id, product.definition.id))
            {
                _spawnedIAPTabsScript[i].OnPurchaseFail(product, failureReason);
            }
        }
        Debug.Log(string.Format("OnPurchaseFailed: FAIL. Product: '{0}', PurchaseFailureReason: {1}", product.definition.storeSpecificId, failureReason));
    }


    void InstantiateIapTabs()
    {
        DestroyAllSpawnedTabs();
        Debug.Log("Insatntiate tabs");
        for (int i = 0; i < _iapDatas.Count; i++)
        {
            _spawnedIapTabs.Add(Instantiate(iapTabPrefab, iapTabsParent));
            _spawnedIAPTabsScript.Add(_spawnedIapTabs[i].GetComponent<IAPTab>());
            _spawnedIAPTabsScript[i].iapData = _iapDatas[i];
            _spawnedIAPTabsScript[i].ChangePriceValue(m_StoreController.products.WithID(_iapDatas[i].in_app_purchase_id).metadata.localizedPriceString);
        }

        UIManager.Instance.HideLoader();
    }

    void DestroyAllSpawnedTabs()
    {
        for (int i = 0; i < _spawnedIapTabs.Count; i++)
        {
            Destroy(_spawnedIapTabs[i]);
            Destroy(_spawnedIAPTabsScript[i]);
        }
        _spawnedIapTabs.Clear();
        _spawnedIAPTabsScript.Clear();

    }
}

[System.Serializable]
public class IAPData
{
    //public DateTime created;
    //public DateTime updated;
    public string _id;
    public string in_app_purchase_id;
    public string title;
    public string description;
    public float price;
    public int chips;
    public string purchase_type;
    //public DateTime start_date;
    //public DateTime end_date;
    public string image;
    public string status;
}
