using UnityEngine;
using AuthNamespace;

public class UnityEmailAuthManagerMonoBehaviour : MonoBehaviour
{
    [Header("Authentication Fields")]
    [SerializeField] private string email = "example@example.com"; // Set your test email here
    [SerializeField] private string password = "password123"; // Set your test password here

    private async void Start()
    {
        // Ensure Unity services are initialized before calling the authentication
        await Unity.Services.Core.UnityServices.InitializeAsync();
        
        // Authenticate with Unity using the provided email and password
        await AuthService.AuthenticateWithEmailPassword(email, password);
    }
}