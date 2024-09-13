using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InitWorldDirection : MonoBehaviour
{
    private void Awake()
    {
        Managers.WorldDirection = transform.forward;
    }
}
