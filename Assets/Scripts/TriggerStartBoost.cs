using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerStartBoost : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other != null && other.gameObject.tag == "Player")
        {
            Managers.WorldMovement.StartBoost();
        }
    }
}
