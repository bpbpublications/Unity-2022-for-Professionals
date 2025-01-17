using UnityEngine;
using ViVoxNameSpace;

public class VivoxController : MonoBehaviour
{
    // The name of the the user who send the messages.
    private string _user;
    
    // The name of the Vivox channel the user will join.
    private string _channel;


    // Instance of the Chat class to handle Vivox functionality.
    private Chat _chatInstance;

    private void Start()
    {
        // Create a new instance of the Chat class.
        _chatInstance = new Chat();

        // Define the user name
        _user = "Josep Alemany";        
        
        // Define the channel name to join.
        _channel = "Unity4Prof";

        // Initialize the chat service with a username and channel name.
        _chatInstance.Init(_user, _channel);
    }

    // Method triggered by a UI button to send a text message to the channel.
    public void ButtonSendTextMessage(string text)
    {
        Debug.Log("CLICK BUTTON to send Message"); // Log when the button is clicked.
        _chatInstance.SendTextMessage(text); // Send the message via the chat instance.
    }
}