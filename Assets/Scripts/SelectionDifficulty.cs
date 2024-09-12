using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectionDifficulty : MonoBehaviour
{
    public static SelectionDifficulty Instance { get; private set; }
    public Difficulty Difficulty { get; set; } = Difficulty.EASY;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else if (Instance != this)
        {
            Destroy(gameObject);
        }
    }
}
