using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    [Header("Audio Sources")]
    [SerializeField] AudioSource musicSource;
    [SerializeField] AudioSource SFXSource;

    [Header("Audio Clips")]
    public AudioClip loseMusic;
    public AudioClip mainMenuMusic;
    public AudioClip winMusic;
    public AudioClip boostSFX;
    public AudioClip BtnSwitchSFX;
    public AudioClip calecheSFX;
    public AudioClip eatingCarrotSFX;
    public AudioClip horseRunningSFX;
    public AudioClip hurtSFX;
    public AudioClip hurt2SFX;
    public AudioClip hurt3SFX;


    public void PlayMusic(AudioClip music)
    {
        musicSource.clip = music;
        musicSource.Play();
    }

    public void PlaySFX(AudioClip sfx)
    {
        SFXSource.PlayOneShot(sfx);
    }
}
