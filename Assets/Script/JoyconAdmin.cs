using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JoyconAdmin : MonoBehaviour
{
    private List<Joycon> joycons;
    public int jc_ind = 0;

    void Start()
    {
        joycons = JoyconManager.Instance.j;
		if (joycons.Count < jc_ind+1){
			Destroy(gameObject);
		}
    }

    
    void Update()
    {
        if (joycons.Count > 0)
        {
            Joycon j = joycons [jc_ind];
            if (j.GetButtonDown (Joycon.Button.DPAD_DOWN)) 
            {
				Debug.Log ("down/B");
            }
            if (j.GetButtonDown (Joycon.Button.DPAD_UP)) 
            {
				Debug.Log ("up/X");
            }
            if (j.GetButtonDown (Joycon.Button.DPAD_LEFT)) 
            {
				Debug.Log ("left/Y");
            }
            if (j.GetButtonDown (Joycon.Button.DPAD_RIGHT)) 
            {
				Debug.Log ("right/A");
            }
            if (j.GetButtonDown (Joycon.Button.SHOULDER_1)) 
            {
				Debug.Log ("SHOULDER_1");
            }
            if (j.GetButtonDown (Joycon.Button.SHOULDER_2)) 
            {
				Debug.Log ("SHOULDER_2");
            }
        }
    }
}
