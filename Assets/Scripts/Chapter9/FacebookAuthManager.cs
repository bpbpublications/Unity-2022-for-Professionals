using UnityEngine;
using AuthNamespace;

public class FacebookAuthManager : MonoBehaviour
{
    private async void Start()
    {
        Debug.Log("Authenticating with Facebook...");
        AuthService.LoginWithFacebook();
    }
}