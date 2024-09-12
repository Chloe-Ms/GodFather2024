using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerDeleteRoadChunk : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other != null)
        {
            Managers.WorldMovement.UpdateChunks();
        }
    }
}
