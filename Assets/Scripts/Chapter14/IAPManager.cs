using UnityEngine;
using UnityEngine.Purchasing;
using UnityEngine.UI; // Import the UI namespace

/// <summary>
/// MonoBehaviour to manage In-App Purchases (IAP) during gameplay.
/// </summary>
public class IAPManager : MonoBehaviour
{
    // Product IDs for the IAP items
    [SerializeField] private string _removeAdsProductId = "remove_ads";
    [SerializeField] private string _premiumUpgradeProductId = "premium_upgrade";

    // Reference to the UI button
    [SerializeField] private Button _purchaseButton;

    /// <summary>
    /// Initializes Unity IAP when the script starts.
    /// </summary>
    void Start()
    {
        // Configure products and initialize IAP
        var builder = ConfigurationBuilder.Instance(StandardPurchasingModule.Instance());
        builder.AddProduct(_removeAdsProductId, ProductType.NonConsumable);
        builder.AddProduct(_premiumUpgradeProductId, ProductType.NonConsumable);

        IAPNamespace.IAPManager.InitializeIAP(builder);

        // Subscribe to purchase events
        IAPNamespace.IAPManager.OnPurchaseSuccess += OnPurchaseSuccess;
        IAPNamespace.IAPManager.OnPurchaseFailure += OnPurchaseFailure;

        // Setup button click event
        if (_purchaseButton != null)
        {
            _purchaseButton.onClick.AddListener(OnPurchaseButtonClicked);
        }
    }

    /// <summary>
    /// Cleans up event subscriptions on destroy.
    /// </summary>
    void OnDestroy()
    {
        IAPNamespace.IAPManager.OnPurchaseSuccess -= OnPurchaseSuccess;
        IAPNamespace.IAPManager.OnPurchaseFailure -= OnPurchaseFailure;

        if (_purchaseButton != null)
        {
            _purchaseButton.onClick.RemoveListener(OnPurchaseButtonClicked);
        }
    }

    /// <summary>
    /// Triggered when the purchase button is clicked.
    /// </summary>
    private void OnPurchaseButtonClicked()
    {
        // Trigger the purchase of the "Remove Ads" product (for example)
        Debug.Log("Purchase button clicked.");
        IAPNamespace.IAPManager.BuyProduct(_removeAdsProductId);  // Replace with your product ID
    }

    /// <summary>
    /// Handles successful purchases.
    /// </summary>
    private void OnPurchaseSuccess(string productId)
    {
        if (productId == _removeAdsProductId)
        {
            Debug.Log("Remove Ads purchased successfully.");
            // Implement functionality to disable ads
        }
        else if (productId == _premiumUpgradeProductId)
        {
            Debug.Log("Premium Upgrade purchased successfully.");
            // Implement functionality for premium upgrade
        }
    }

    /// <summary>
    /// Handles failed purchases.
    /// </summary>
    private void OnPurchaseFailure(string productId, PurchaseFailureReason reason)
    {
        Debug.LogError($"Purchase failed for product: {productId}, Reason: {reason}");
    }
}