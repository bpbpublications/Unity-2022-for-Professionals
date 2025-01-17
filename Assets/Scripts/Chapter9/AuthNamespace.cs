using UnityEngine;
using Unity.Services.Authentication;
using Unity.Services.Core;
using UnityEngine.UI;
using System.Threading.Tasks;
using Facebook.Unity;

namespace AuthNamespace
{
    public static class AuthService
    {
        /// <summary>
        /// Authenticates the user anonymously with Unity Authentication.
        /// </summary>
        public static async void AuthenticateUser()
        {
            if (!AuthenticationService.Instance.IsSignedIn)
            {
                try
                {
                    await AuthenticationService.Instance.SignInAnonymouslyAsync();
                    Debug.Log($"User authenticated successfully. Player ID: {AuthenticationService.Instance.PlayerId}");
                }
                catch (RequestFailedException ex)
                {
                    Debug.LogError($"Error during anonymous authentication: {ex.Message}");
                }
            }
            else
            {
                Debug.Log("The user is already authenticated.");
            }
        }

        /// <summary>
        /// Authenticates the user using Facebook access token.
        /// </summary>
        public static async void AuthenticateWithFacebook(string accessToken)
        {
            if (!AuthenticationService.Instance.IsSignedIn)
            {
                try
                {
                    await AuthenticationService.Instance.SignInWithFacebookAsync(accessToken);
                    Debug.Log($"User authenticated via Facebook. Player ID: {AuthenticationService.Instance.PlayerId}");
                }
                catch (RequestFailedException ex)
                {
                    Debug.LogError($"Error during Facebook authentication: {ex.Message}");
                }
            }
            else
            {
                Debug.Log("The user is already authenticated.");
            }
        }

        /// <summary>
        /// Logs in with Facebook, retrieves the access token, and authenticates with Unity Services.
        /// </summary>
        public static void LoginWithFacebook()
        {
            if (!FB.IsInitialized)
            {
                FB.Init(() =>
                {
                    Debug.Log("Facebook SDK initialised.");
                    PerformFacebookLogin();
                });
            }
            else
            {
                PerformFacebookLogin();
            }
        }

        private static void PerformFacebookLogin()
        {
            FB.LogInWithReadPermissions(new[] { "public_profile", "email" }, result =>
            {
                if (FB.IsLoggedIn)
                {
                    string accessToken = AccessToken.CurrentAccessToken.TokenString;
                    Debug.Log($"Facebook Access Token: {accessToken}");

                    // Authenticate with Unity Services using the retrieved token
                    AuthenticateWithFacebook(accessToken);
                }
                else
                {
                    Debug.LogError("Failed to log in to Facebook.");
                }
            });
        }

        /// <summary>
        /// Authenticates the user using Google Sign-In.
        /// </summary>
        public static async void AuthenticateWithGoogle(string idToken)
        {
            if (!AuthenticationService.Instance.IsSignedIn)
            {
                try
                {
                    await AuthenticationService.Instance.SignInWithGoogleAsync(idToken);
                    Debug.Log($"User authenticated via Google. Player ID: {AuthenticationService.Instance.PlayerId}");
                }
                catch (RequestFailedException ex)
                {
                    Debug.LogError($"Error during Google authentication: {ex.Message}");
                }
            }
            else
            {
                Debug.Log("The user is already authenticated.");
            }
        }

        /// <summary>
        /// Authenticates the user using Apple Sign-In.
        /// </summary>
        public static async void AuthenticateWithApple(string idToken)
        {
            if (!AuthenticationService.Instance.IsSignedIn)
            {
                try
                {
                    await AuthenticationService.Instance.SignInWithAppleAsync(idToken);
                    Debug.Log($"User authenticated via Apple. Player ID: {AuthenticationService.Instance.PlayerId}");
                }
                catch (RequestFailedException ex)
                {
                    Debug.LogError($"Error during Apple authentication: {ex.Message}");
                }
            }
            else
            {
                Debug.Log("The user is already authenticated.");
            }
        }

        /// <summary>
        /// Authenticates the user using Unity Email and Password.
        /// </summary>
        public static async Task AuthenticateWithEmailPassword(string email, string password)
        {
            try
            {
                // This method should be used for Unity authentication
                await AuthenticationService.Instance.AddUsernamePasswordAsync(email, password);
                Debug.Log($"User authenticated successfully with email. Player ID: {AuthenticationService.Instance.PlayerId}");
            }
            catch (RequestFailedException ex)
            {
                Debug.LogError($"Error during email/password authentication: {ex.Message}");
            }
        }
    }

    public class UnityEmailAuthManager : MonoBehaviour
    {
        [Header("Authentication Fields")]
        [SerializeField] private string email;
        [SerializeField] private string password;
        [SerializeField] private Text statusText; // Display login status for the user

        private async void Start()
        {
            if (string.IsNullOrEmpty(email) || string.IsNullOrEmpty(password))
            {
                Debug.LogError("Email and Password must be provided.");
                return;
            }

            await Unity.Services.Core.UnityServices.InitializeAsync();
            Debug.Log("Attempting to authenticate with Unity using email and password...");

            // Call Unity Authentication method to authenticate with email & password
            await AuthenticateWithEmailPassword(email, password);
        }

        /// <summary>
        /// Authenticates the user with Unity Authentication using email and password.
        /// </summary>
        private async Task AuthenticateWithEmailPassword(string email, string password)
        {
            try
            {
                // Authenticate the user
                await AuthenticationService.Instance.AddUsernamePasswordAsync(email, password);
                statusText.text = "Authentication successful!";
                Debug.Log($"User authenticated successfully. Player ID: {AuthenticationService.Instance.PlayerId}");
            }
            catch (RequestFailedException ex)
            {
                statusText.text = $"Authentication failed: {ex.Message}";
                Debug.LogError($"Authentication failed: {ex.Message}");
            }
        }
    }
}