using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HumainSFX : MonoBehaviour
{
    AudioManager audioManager;
    Collider _collision;

    public void Awake()
    {
        audioManager = GameObject.FindGameObjectWithTag("Audio").GetComponent<AudioManager>();
        _collision = GetComponent<Collider>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other != null && other.gameObject.tag == "Player")
        {
            audioManager.PlaySFX(audioManager.hurt2SFX);   
        }
    }
}
