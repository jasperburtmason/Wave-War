using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Menu : MonoBehaviour
{
    public string playSceneName;

    public void Play()
    {
        SceneManager.LoadSceneAsync(playSceneName);
    }

    public void Quit()
    {
        Application.Quit();
    }
}
