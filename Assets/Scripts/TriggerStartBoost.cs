using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerStartBoost : MonoBehaviour
{
    AudioManager audioManager;

    public void Awake()
    {
        audioManager = GameObject.FindGameObjectWithTag("Audio").GetComponent<AudioManager>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other != null && other.gameObject.tag == "Player")
        {
            Managers.WorldMovement.StartBoost();
            audioManager.PlaySFX(audioManager.boostSFX);
        }
    }
}
