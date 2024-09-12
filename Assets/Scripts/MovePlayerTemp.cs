using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovePlayerTemp : MonoBehaviour
{
    void Update()
    {
        if (Input.GetKey(KeyCode.LeftArrow))
        {
            transform.position -= transform.right * Time.deltaTime * 5;
        }
        if (Input.GetKey(KeyCode.RightArrow))
        {
            transform.position += transform.right * Time.deltaTime * 5;
        }
    }
}
