using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomImpulse : MonoBehaviour
{
    public ForceMode ForceApplicationMode;
    public float impulsePower = 1.0f;

    // Use this for initialization
    void Start ()
    {
        InvokeRepeating("FireImpulse", 1.0f, 3.14f);
        //FireImpulse();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    void FireImpulse()
    {
        gameObject.GetComponent<Rigidbody>().AddForce(Random.insideUnitSphere * impulsePower, ForceApplicationMode);
    }
}
