using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

public class NetworkManagerUI : MonoBehaviour
{
    public NetworkManager networkManager;
    public InputField matchNameField;

    public void StartHost()
    {
        networkManager.matchName = matchNameField.text;
        networkManager.StartHost();
    }
}
