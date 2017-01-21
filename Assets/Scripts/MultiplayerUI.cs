using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

public class MultiplayerUI : MonoBehaviour
{
    public MultiplayerManager multiplayerManager;
    public InputField ipField;
    public InputField portField;

    public void CreateServer()
    {
        multiplayerManager.CreateServer(int.Parse(portField.text));
    }

    public void JoinServer()
    {

    }
}
