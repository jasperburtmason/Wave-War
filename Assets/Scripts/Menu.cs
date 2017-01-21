using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Menu : MonoBehaviour
{
    public string playSceneName;
    public string backSceneName;

    public void Play()
    {
        SceneManager.LoadSceneAsync(playSceneName);
    }

    public void Back()
    {
        SceneManager.LoadSceneAsync(backSceneName);
    }

    public void Quit()
    {
        Application.Quit();
    }
}
