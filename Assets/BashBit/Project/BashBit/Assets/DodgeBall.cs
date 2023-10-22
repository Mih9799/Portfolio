using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DodgeBall : MonoBehaviour {

    public float throwPower = 5;

	// Use this for initialization
	void Start ()
    {
        //instantly apply an Impulse to the rigidbody, therefore throwiong it away from its spawn location
        gameObject.GetComponent<Rigidbody>().AddForce(transform.forward * throwPower, ForceMode.Impulse);
        //print("got thrown");
	}

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            print("Hit Player");
            Destroy(gameObject);
        }
    }
}
