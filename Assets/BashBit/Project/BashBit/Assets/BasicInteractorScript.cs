using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicInteractorScript : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update ()
    {
        //If the player has pressed the interaction key, THEN we will perform a raycast to see if we hit an interactable
        if (Input.GetKeyDown(KeyCode.E))
        {
            RaycastHit hit;

            Debug.DrawLine(transform.position, transform.position + (this.transform.forward * 1), Color.green, 15, false);

            if (Physics.Raycast(transform.position, this.transform.forward, out hit, 5.0f))
            {
                if (hit.collider.gameObject.GetComponent<InteractorSwitch>())
                {
                    hit.collider.gameObject.GetComponent<InteractorSwitch>().toggleSwitch();
                }
            }
                
        }
	}
}
