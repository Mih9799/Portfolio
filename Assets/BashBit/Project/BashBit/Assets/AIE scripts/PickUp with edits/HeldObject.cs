using UnityEngine;
using System.Collections;

public class HeldObject : MonoBehaviour 
{

	public ObjectPickUp PickUpScript;
	void  OnTriggerEnter(Collider other)
	{
		foreach(string TAG in PickUpScript.BlockerTags)
		{
			if(other.gameObject.CompareTag(TAG))
			{
				//PickUpScript.DropObject();	
				break;
			}
		}
	}
	
	public void DestroyMe()
	{
		Destroy(this);
	}
}
