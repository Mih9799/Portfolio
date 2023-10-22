using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interactor : MonoBehaviour {

    public bool isInteractionEnabled = true;
    public float interactionDistance = 3.0f;
    RaycastHit traceHit;
    Ray interactionRay;

    public List<Interactable> interactableComponents;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update ()
    {
        if (Input.GetButtonDown("Fire1"))
        {
            if (Physics.Raycast(Camera.main.transform.position, Camera.main.transform.forward, out traceHit, interactionDistance))
            {
                if (traceHit.collider.gameObject.GetComponent<Interactable>())
                {
                    foreach (Interactable intComp in traceHit.collider.gameObject.GetComponents<Interactable>())
                    {
                        intComp.StartInteraction();
                        interactableComponents.Add(intComp);
                    }
                }
                print(traceHit.collider.name);
            }
            print("No Hit Detected");
        }
        if (Input.GetButton("Fire1"))
        {
            if (interactableComponents.Count >-1)
            {
                foreach (Interactable intComp in interactableComponents)
                {
                    intComp.TickInteraction();
                }
            }

        }
        if (Input.GetButtonUp("Fire1"))
        {
            if (interactableComponents.Count > -1)
            {
                foreach (Interactable intComp in interactableComponents)
                {
                    intComp.StopInteraction();
                }
            }

            interactableComponents.Clear();
        }            
	}
}
