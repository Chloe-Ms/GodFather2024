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
    private Vector3 defaultPosition;
    
    void Start()
    {
        defaultPosition = transform.position;
        gyro = new Vector3(0, 0, 0);
        realPosition = gameObject.transform.position;
        // get the public Joycon array attached to the JoyconManager in scene
        if (JoyconManager.Instance != null )
            joycons = JoyconManager.Instance.j;
		if (joycons != null && joycons.Count < jc_ind+1){
			Destroy(gameObject);
		}
    }

    
    void Update()
    {
        if (joycons != null && joycons.Count > 0)
        {
            Joycon j = joycons [jc_ind];
            if (j.GetButtonDown(Joycon.Button.SHOULDER_2) || Input.GetKeyDown(KeyCode.Space))
            {
                //Debug.Log ("Shoulder button 2 pressed");
                realPosition = defaultPosition;
            }
            gyro = j.GetGyro();
            realPosition.x += (gyro.x/10);

            if (realPosition.x < minX)
            {
                newPosition.x = minX;
            }else if (realPosition.x > maxX)
            {
                newPosition.x = maxX;
            }else
            {
                newPosition.x = realPosition.x;
            }
            gameObject.transform.position = newPosition;

            
        }
    }
}
