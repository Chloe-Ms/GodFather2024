using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JoyconMouv : MonoBehaviour
{
    private List<Joycon> joycons;

    public Vector3 gyro;
    public int jc_ind = 0;
    [SerializeField] private float minX;
    [SerializeField] private float maxX;
    private Vector3 newPosition;
    private Vector3 realPosition;
    [SerializeField] private GameObject defaultPosition;
    
    void Start()
    {
        gyro = new Vector3(0, 0, 0);
        realPosition = gameObject.transform.position;
        // get the public Joycon array attached to the JoyconManager in scene
        if (JoyconManager.Instance != null )
            joycons = JoyconManager.Instance.j;
		if (joycons != null && joycons.Count < jc_ind+1){
			Destroy(gameObject);
		}
        transform.position = defaultPosition.transform.position;
    }

    
    void Update()
    {
        if (joycons != null && joycons.Count > 0)
        {
            Joycon j = joycons [jc_ind];

            gyro = j.GetAccel();
            Debug.Log(gyro);
            if (gyro.y < 0.05 && gyro.y > -0.05)
            {
                gyro.y = 0;
            }
            realPosition.x += (gyro.y/3);

            if (realPosition.x < minX)
            {
                realPosition.x = minX;
            }else if (realPosition.x > maxX)
            {
                realPosition.x = maxX;
            }else
            {
                newPosition.x = realPosition.x;
            }
            gameObject.transform.position = newPosition;

            
        }
    }
}
