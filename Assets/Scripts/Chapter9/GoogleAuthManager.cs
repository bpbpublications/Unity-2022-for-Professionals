using UnityEngine;
using AuthNamespace;

public class GoogleAuthManager : MonoBehaviour
{
    [SerializeField]
    private string googleIdToken; // Input your Google ID Token here

    private async void Start()
    {
        if (string.IsNullOrEmpty(googleIdToken))
        {
            Debug.LogError("Google ID Token is not set!");
            return;
        }

        Debug.Log("Authenticating with Google...");
        AuthService.AuthenticateWithGoogle(googleIdToken);
    }
}