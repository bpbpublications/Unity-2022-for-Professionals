namespace AdsNamespace
{
    using UnityEngine;
    using UnityEngine.Advertisements;

    /// <summary>
    /// Handles all Unity Ads banner-related functions.
    /// </summary>
    public static class BannerAds
    {
        // Platform-specific Ad Unit IDs
        private static string _adUnitId;

        /// <summary>
        /// Initializes the Ad Unit ID based on the platform.
        /// </summary>
        public static void InitializeBanner(string androidAdUnitId, string iosAdUnitId)
        {
            _adUnitId = (Application.platform == RuntimePlatform.IPhonePlayer)
                ? iosAdUnitId
                : androidAdUnitId;

            Advertisement.Banner.SetPosition(BannerPosition.BOTTOM_CENTER);
        }

        /// <summary>
        /// Loads the banner ad and sets callbacks.
        /// </summary>
        public static void LoadBanner(System.Action onLoaded, System.Action<string> onError)
        {
            Advertisement.Banner.Load(_adUnitId, new BannerLoadOptions
            {
                loadCallback = () => onLoaded?.Invoke(),
                errorCallback = (message) => onError?.Invoke(message)
            });
        }

        /// <summary>
        /// Displays the banner ad with interaction callbacks.
        /// </summary>
        public static void ShowBanner(System.Action onShown, System.Action onClicked, System.Action onHidden)
        {
            Advertisement.Banner.Show(_adUnitId, new BannerOptions
            {
                showCallback = () => onShown?.Invoke(),
                clickCallback = () => onClicked?.Invoke(),
                hideCallback = () => onHidden?.Invoke()
            });
        }

        /// <summary>
        /// Hides the banner ad.
        /// </summary>
        public static void HideBanner()
        {
            Advertisement.Banner.Hide();
        }
    }
}