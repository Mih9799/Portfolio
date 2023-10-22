using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class PointAndClickPawnControls : MonoBehaviour {
    RaycastHit traceHit;
    Vector3 moveDestination;
    NavMeshAgent agent;

    // Use this for initialization
    void Start ()
    {
        if (GetComponent<NavMeshAgent>())
        {
            agent = GetComponent<NavMeshAgent>();
        }
        else
        {
            Destroy(this);
        }        
	}
	
	// Update is called once per frame
	void Update () {
        if (Input.GetButtonDown("Fire1"))
        {
            traceForMovement();
            
        }
		
	}

    public void traceForMovement()

    {
        print("Tracing:...");
        //trace from the camera point to the clikced point on screen


        Ray traceRay = Camera.main.ScreenPointToRay(Input.mousePosition);

        Physics.Raycast(traceRay, out traceHit);

        if (checkNavDestination())
        {
            agent.SetDestination(moveDestination);
        }
        else
        {
            print("No valid Destination Found");
        }
       

    }

    bool checkNavDestination()
    {
        if (traceHit.collider)
        {
            print("Hit something!");
            UnityEngine.AI.NavMeshHit hit;
            if (UnityEngine.AI.NavMesh.SamplePosition(traceHit.point, out hit, 1.0f, UnityEngine.AI.NavMesh.AllAreas))
            {
                moveDestination = hit.position;
                Debug.DrawLine(hit.position, hit.position + new Vector3(0, 2, 0));
                return true;
            }
            else
            {
                return false;
            }
        }
        else
        {
            return false;
        }
    }

}
