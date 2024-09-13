using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicMainMenu : MonoBehaviour
{
    AudioManager audioManager;
        public void Awake()
    {
        audioManager = GameObject.FindGameObjectWithTag("Audio").GetComponent<AudioManager>();
    }
    void Start()
    {
        audioManager.PlayMusic(audioManager.mainMenuMusic);
    }
}
