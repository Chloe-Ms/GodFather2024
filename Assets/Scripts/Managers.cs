using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Managers : MonoBehaviour
{
    public static ForwardWorldMovement WorldMovement { get; set; }
    public static ManagerRoad ManagerRoad { get; set; }
    public static PlayerCollision PlayerCollision { get; set; }
    public static ManagerEndings ManagerEndings { get; set; }
    public static Vector3 WorldDirection { get; set; }
}
