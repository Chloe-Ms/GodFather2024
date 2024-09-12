using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectDifficulty : MonoBehaviour
{
    [SerializeField] Difficulty _dificulty;
    [SerializeField] SceneManagement _sceneManagement;

    public void ChangeDifficulty()
    {
        SelectionDifficulty.Instance.Difficulty = _dificulty;
        _sceneManagement.LoadMainScene();
    }
}
