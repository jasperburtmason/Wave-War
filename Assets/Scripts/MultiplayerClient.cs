using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class NetClient
{
    NetworkClient myClient;

    public void OnConnected(NetworkMessage message)
    {
        Debug.Log("Connected to server");
        //myClient
    }

    public void OnDisconnected(NetworkMessage message)
    {
        Debug.Log("Disconnected from server");
    }

    public void OnError(NetworkMessage message)
    {
        Debug.Log("Error connecting with code ");
    }

    public void Start()
    {
        myClient = new NetworkClient();

        myClient.RegisterHandler(MsgType.Connect, OnConnected);
        myClient.RegisterHandler(MsgType.Disconnect, OnDisconnected);
        myClient.RegisterHandler(MsgType.Error, OnError);

        ClientScene.Ready(myClient.connection);
        Network.Connect("127.0.0.1", 8888);
    }
}