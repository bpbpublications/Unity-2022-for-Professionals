using UnityEngine;
using AuthNamespace;

public class UnityAuthManager : MonoBehaviour
{
    private async void Start()
    {
        Debug.Log("Authenticating anonymously...");
        AuthService.AuthenticateUser();
    }
}