using UnityEngine;
using Unity.Services.Authentication;
using Unity.Services.Core;
using Unity.Services.Vivox;

namespace ViVoxNameSpace
{
    // This class handles chat functionality using Unity's Vivox service.
    public class Chat
    {
        // Private variables to store the username and channel name.
        private static bool Joined;
        private static string _userName;
        private static string _channelName;

        // Initializes the chat service with the given username and channel name.
        // Calls the Initialization method to start the service.
        public void Init(string user, string channel)
        {
            Joined = false;   // Control si the user is joined to the channel before sending a message
            _userName = user; // Set the username for the session.
            _channelName = channel; // Set the channel name to join.
            Initialization(); // Start the Vivox service initialization process.
        }
        
        // Handles the asynchronous initialization of Unity Services, Authentication, and Vivox.
        private async void Initialization()
        {
            await UnityServices.InitializeAsync(); // Initialize Unity Services.
            await AuthenticationService.Instance.SignInAnonymouslyAsync(); // Authenticate anonymously.
            await VivoxService.Instance.InitializeAsync(); // Initialize the Vivox service.
            Login(); // Proceed to login after initialization.
        }
        
        // Logs the user into Vivox and joins a channel.
        private async void Login()
        {
            // Configure Vivox login options.
            LoginOptions myOptions = new LoginOptions
            {
                DisplayName = _userName, // Set the display name.
                EnableTTS = true // Enable Text-to-Speech functionality.
            };

            await VivoxService.Instance.LoginAsync(myOptions); // Perform the login.
            JoinToChannel(); // Join the specified channel after logging in.
            Debug.Log("You are logged to Vivox, " + _userName); // Confirm successful login in the console.
        }        
        
        // Joins a specified Vivox channel with chat capabilities (text and audio).
        private async void JoinToChannel()
        {
            ChatCapability _chatCapability = ChatCapability.TextAndAudio; // Define chat capabilities.
            await VivoxService.Instance.JoinEchoChannelAsync(_channelName, _chatCapability); // Join the channel.
            Joined = true;
            Debug.Log("You are chatting on channel: " + _channelName); // Confirm channel join in the console.
            SendTextMessage("Hi there! You are doing well!"); // Send an initial greeting message to the channel.
        }

        // Sends a text message to the current channel.
        public void SendTextMessage(string text)
        {
            if (Joined)         // Check if the user is already joined to the channel before sending the message.
            {
                VivoxService.Instance.SendChannelTextMessageAsync(_channelName, text); // Send the message to the channel.
                Debug.Log("Message sent: " + text + " to channel " + _channelName); // Log the message for debugging.
            }
        }
    }
}