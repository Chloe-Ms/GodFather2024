using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerRoadEnd : MonoBehaviour
{
    private void OnTriggerEnter(Collider collider)
    {
        if (collider != null)
        {
            ForwardPlayerMovement playerForwardMvt = collider.GetComponent<ForwardPlayerMovement>();
            playerForwardMvt?.StopDecelerateMovingForward();
        }
    }
}
