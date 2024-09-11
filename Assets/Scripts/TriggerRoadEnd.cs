using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerRoadEnd : MonoBehaviour
{
    private void OnTriggerEnter(Collider collider)
    {
        if (collider != null)
        {
            Managers.WorldMovement?.StopDecelerateMovingForward();
        }
    }
}
