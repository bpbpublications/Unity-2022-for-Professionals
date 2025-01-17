using UnityEngine;
using AuthNamespace;

public class AppleAuthManager : MonoBehaviour
{
    [SerializeField]
    private string appleIdToken; // Input your Apple ID Token here

    private async void Start()
    {
        if (string.IsNullOrEmpty(appleIdToken))
        {
            Debug.LogError("Apple ID Token is not set!");
            return;
        }

        Debug.Log("Authenticating with Apple...");
        AuthService.AuthenticateWithApple(appleIdToken);
    }
}