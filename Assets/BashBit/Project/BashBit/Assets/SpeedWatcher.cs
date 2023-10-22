using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpeedWatcher : MonoBehaviour {

    Rigidbody thisRB;

	// Use this for initialization
	void Start () {
        thisRB = GetComponent<Rigidbody>();
	}
	
	// Update is called once per frame
	void Update () {
        Debug.Log(thisRB.velocity.magnitude);
	}
}
