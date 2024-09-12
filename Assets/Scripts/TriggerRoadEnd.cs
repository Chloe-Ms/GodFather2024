using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerRoadEnd : MonoBehaviour
{
    private void OnTriggerEnter(Collider collider)
    {
        if (collider != null && collider.gameObject.tag == "Player")
        {
            Managers.WorldMovement?.StopDecelerateMovingForward();
        }
    }
}
