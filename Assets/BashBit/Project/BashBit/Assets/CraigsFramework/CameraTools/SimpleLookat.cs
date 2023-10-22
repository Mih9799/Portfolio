using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimpleLookat : MonoBehaviour {
    public bool selectPlayerByTag = true;
    public Transform target;
    public bool useSoftTargeting;
    public float trackingSpeed = 1.0f;

    // Use this for initialization
    void Start () {
        if (selectPlayerByTag)
        {
            target = GameObject.FindGameObjectWithTag("Player").transform;
        }
	}
	
	// Update is called once per frame
	void Update () {
        if (target)
        {
            TrackTarget();
        }
		
	}

    void TrackTarget()
    {
        if (!useSoftTargeting)
        {
            transform.LookAt(target);
        }
        else
        {
            //this will orient this gameobject to look at another
            transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.LookRotation(target.position - this.transform.position, Vector3.up), (Time.deltaTime * trackingSpeed));
        }
        
    }
}
