using Unity.Netcode;
using UnityEngine;
namespace NetCodeNameSpace
	{
	    public class NetCodedManager : MonoBehaviour
	    {
	        void OnGUI()
	        {
	            GUILayout.BeginArea(new Rect(10, 10, 300, 300));
	            if (!NetworkManager.Singleton.IsClient && !NetworkManager.Singleton.IsServer)
	            {
                 InitUI();
	            }
	            else
	            {
	                UpdateUI();
					SendLocation();
	            }
	
	            GUILayout.EndArea();
	        }
	
	        static void InitUI()
	        {
				if (GUILayout.Button("Host")) NetworkManager.Singleton.StartHost();
	            if (GUILayout.Button("Client")) NetworkManager.Singleton.StartClient();
	            if (GUILayout.Button("Server")) NetworkManager.Singleton.StartServer();
	        }
	
	        static void UpdateUI()
	        {
	            var type = NetworkManager.Singleton.IsHost ?
	                "Host" : NetworkManager.Singleton.IsServer ? "Server" : "Client";
	
	            GUILayout.Label("Unity Transport: " +
	                NetworkManager.Singleton.NetworkConfig.NetworkTransport.GetType().Name);
	            GUILayout.Label("Type: " + type);
	        }
	
	        static void SendLocation()
	        {
	            if (GUILayout.Button(NetworkManager.Singleton.IsServer ? "Move" : "Request Location"))
	            {
	                if (NetworkManager.Singleton.IsServer && !NetworkManager.Singleton.IsClient )
	                {
	                    foreach (ulong uid in NetworkManager.Singleton.ConnectedClientsIds)
	                        NetworkManager.Singleton.SpawnManager.GetPlayerNetworkObject(uid).GetComponent<PlayerNetwork>().Move();
	                }
	                else
	                {
	                    var playerObject = NetworkManager.Singleton.SpawnManager.GetLocalPlayerObject();
	                    var player = playerObject.GetComponent<PlayerNetwork>();
	                    player.Move();
	                }
	            }
	        }
	    }
	}