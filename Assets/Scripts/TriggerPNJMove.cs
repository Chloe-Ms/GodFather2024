using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerPNJMove : MonoBehaviour
{
    [SerializeField] PNJMovement _pnjMovement;

    private void OnTriggerEnter(Collider other)
    {
        if (other != null && other.gameObject.tag == "Player")
        {
            _pnjMovement.Move();
        }
    }
}
