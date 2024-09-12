using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerRoadEnd : MonoBehaviour
{
    private void OnTriggerEnter(Collider collider)
    {
        Debug.Log($"{collider.gameObject.tag}");
        if (collider != null && collider.gameObject.tag == "Player")
        {
            Debug.Log("COUCO");
            Managers.WorldMovement?.StopDecelerateMovingForward();
        }
    }
}
