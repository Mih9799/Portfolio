using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public enum DestType
{
	InOrderLoop,
	RandomOrder,
};

[AddComponentMenu("AIE Scripts/Teleportation/Trigger Teleportaion Advanced")]
public class TriggerTeleportationAdvanced : MonoBehaviour {
	
	public List<string> TriggerTags = new List<string>();	

	public DestType DestinationOrder;
	public List<Transform> Destination;

	int Currentindex = 0;
	
	// Use this for initialization
	void Start () 
	{
		if(!(Destination.Count > 0))
		{
			print("ERROR - " + gameObject.name + " - No Destination Set!!");
		}
	}
	
	void OnTriggerEnter(Collider collided)
	{
		if(TriggerTags.Count > 0)
		{
			foreach(string TAG in TriggerTags)
			{
				if(collided.gameObject.CompareTag(TAG))
				{
					if(DestinationOrder == DestType.RandomOrder)
					{
						collided.transform.position = Destination[Random.Range(0,Destination.Count)].position;
						if(collided.GetComponent<HeldObject>() != null)
						{
							collided.GetComponent<HeldObject>().PickUpScript.DropObject();
						}
					}
					else if (DestinationOrder == DestType.InOrderLoop)
					{
						collided.transform.position = Destination[Currentindex].position;
						if(collided.GetComponent<HeldObject>() != null)
						{
							collided.GetComponent<HeldObject>().PickUpScript.DropObject();
						}
						Currentindex++;
						if(Currentindex >=  Destination.Count)
						{
							Currentindex = 0;
						}
					}
					break;
				}
			}
		}
		else
		{
			if(DestinationOrder == DestType.RandomOrder)
			{
				collided.transform.position = Destination[Random.Range(0,Destination.Count)].position;
				if(collided.GetComponent<HeldObject>() != null)
				{
					collided.GetComponent<HeldObject>().PickUpScript.DropObject();
				}
			}
			else if (DestinationOrder == DestType.InOrderLoop)
			{
				collided.transform.position = Destination[Currentindex].position;
				if(collided.GetComponent<HeldObject>() != null)
				{
					collided.GetComponent<HeldObject>().PickUpScript.DropObject();
				}
				Currentindex++;
				if(Currentindex >=  Destination.Count)
				{
					Currentindex = 0;
				}
			}
		}
	}
}
