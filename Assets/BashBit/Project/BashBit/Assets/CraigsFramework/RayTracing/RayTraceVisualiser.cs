using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RayTraceVisualiser : MonoBehaviour {
    public float traceDistance = 2.0f;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        PerformRaytrace();
	}


    //this method will perform a Ray trace along this object's forward direction and return any hit found
    public void PerformRaytrace()
    {
        RaycastHit traceHitInfo;

        if (Physics.Raycast(Camera.main.transform.position, Camera.main.transform.forward * traceDistance, out traceHitInfo, traceDistance))
        {
            //perform action based on trace, activate, grab, getcomponent, etc..
        }

        Debug.DrawRay(Camera.main.transform.position, Camera.main.transform.forward * traceDistance, Color.red, Time.deltaTime);
    }
}
