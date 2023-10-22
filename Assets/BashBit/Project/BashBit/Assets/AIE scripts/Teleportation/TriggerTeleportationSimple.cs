using UnityEngine;
using System.Collections;
using System.Collections.Generic;

[AddComponentMenu("AIE Scripts/Teleportation/Trigger Teleportaion Simple")]
public class TriggerTeleportationSimple : MonoBehaviour 
{
	public List<string> TriggerTags = new List<string>();	

	public Transform Destination;

	// Use this for initialization
	void Start () 
	{
		
	}


	void OnTriggerEnter(Collider collided)
	{
		if(TriggerTags.Count > 0)
		{
			foreach(string TAG in TriggerTags)
			{ 
				if(collided.gameObject.CompareTag(TAG))
				{
					collided.transform.position = Destination.position;
					if(collided.GetComponent<HeldObject>() != null)
					{
						collided.GetComponent<HeldObject>().PickUpScript.DropObject();
					}
					break;
				}
			}
		}
		else
		{
			collided.transform.position = Destination.position;
			if(collided.GetComponent<HeldObject>() != null)
			{
				collided.GetComponent<HeldObject>().PickUpScript.DropObject();
			}
		}
	}
}
