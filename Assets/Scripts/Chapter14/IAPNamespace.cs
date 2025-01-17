namespace IAPNamespace
{
    using UnityEngine;
    using UnityEngine.Purchasing;
    using System;

    /// <summary>
    /// Handles all Unity IAP functions.
    /// </summary>
    public static class IAPManager
    {
        // Events for purchase success and failure
        public static event Action<string> OnPurchaseSuccess;
        public static event Action<string, PurchaseFailureReason> OnPurchaseFailure;

        // Store controller for managing purchases
        private static IStoreController _storeController;

        /// <summary>
        /// Initializes Unity IAP with the provided configuration.
        /// </summary>
        public static void InitializeIAP(ConfigurationBuilder builder)
        {
            if (_storeController == null)
            {
                UnityPurchasing.Initialize(new IAPListener(), builder);
            }
        }

        /// <summary>
        /// Processes a purchase request for the given product ID.
        /// </summary>
        public static void BuyProduct(string productId)
        {
            if (_storeController != null && _storeController.products.WithID(productId) != null)
            {
                _storeController.InitiatePurchase(productId);
            }
            else
            {
                Debug.LogError($"Product {productId} not found or store controller is not initialized.");
            }
        }

        /// <summary>
        /// Handles successful purchases.
        /// </summary>
        public static void HandlePurchaseSuccess(string productId)
        {
            Debug.Log($"Purchase successful for product: {productId}");
            OnPurchaseSuccess?.Invoke(productId);
        }

        /// <summary>
        /// Handles failed purchases.
        /// </summary>
        public static void HandlePurchaseFailure(string productId, PurchaseFailureReason reason)
        {
            Debug.LogError($"Purchase failed for product: {productId}, Reason: {reason}");
            OnPurchaseFailure?.Invoke(productId, reason);
        }

        /// <summary>
        /// Custom Unity IAP listener to process purchases and failures.
        /// </summary>
        private class IAPListener : IStoreListener
        {
            /// <summary>
            /// Called when Unity IAP initializes successfully.
            /// </summary>
            public void OnInitialized(IStoreController controller, IExtensionProvider extensions)
            {
                Debug.Log("Unity IAP initialized successfully.");
                IAPManager._storeController = controller; // Set the store controller
            }

            /// <summary>
            /// Called when Unity IAP fails to initialize.
            /// </summary>
            public void OnInitializeFailed(InitializationFailureReason error)
            {
                Debug.LogError($"Unity IAP initialization failed: {error}");
            }

            public void OnInitializeFailed(InitializationFailureReason error, string message)
            {
                throw new NotImplementedException();
            }

            /// <summary>
            /// Processes successful purchases.
            /// </summary>
            public PurchaseProcessingResult ProcessPurchase(PurchaseEventArgs args)
            {
                IAPManager.HandlePurchaseSuccess(args.purchasedProduct.definition.id);
                return PurchaseProcessingResult.Complete; // Mark the purchase as complete
            }

            /// <summary>
            /// Called when a purchase fails.
            /// </summary>
            public void OnPurchaseFailed(Product product, PurchaseFailureReason failureReason)
            {
                IAPManager.HandlePurchaseFailure(product.definition.id, failureReason);
            }
        }
    }
}