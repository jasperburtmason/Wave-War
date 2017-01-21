using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class MultiplayerManager : MonoBehaviour
{
    public Object scene;

    public void CreateServer(int port)
    {
        bool useNat = !Network.HavePublicAddress();
        NetworkConnectionError error = Network.InitializeServer(32, 25000, useNat);
        Debug.Log(error.ToString());
    }

    public void JoinServer(string ip, int port)
    {
        Network.Connect(ip, port);
    }

    public void OnPlayerReadyMessage(NetworkMessage netMsg)
    {
    }
}
