using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneManagement : MonoBehaviour
{
    [SerializeField] string _mainScene;
    [SerializeField] string _menuScene;

    public void LoadMainScene()
    {
        SceneManager.LoadScene(_mainScene);
    }

    public void LoadMenuScene()
    {
        SceneManager.LoadScene(_menuScene);
    }

    public void Restart()
    {
        LoadMainScene();
    }

    public void ExitGame()
    {
        Application.Quit();
    }
}
