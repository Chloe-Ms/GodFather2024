using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Obstacle : MonoBehaviour
{
    Collider _collision;

    private void Awake()
    {
        _collision = GetComponent<Collider>();
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other != null && other.transform.tag == "Player")
        {
            PlayerCollision playerCol = other.GetComponent<PlayerCollision>();
            if (playerCol != null)
            {
                _collision.enabled = false;
                gameObject.SetActive(false);
                playerCol.CollideWithObstacle();
            }
            ShakeCollision shakeCollision = other.GetComponent<ShakeCollision>();
            shakeCollision?.StartShake();
        }

    }
}
