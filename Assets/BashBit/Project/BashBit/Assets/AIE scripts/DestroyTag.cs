using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class DestroyTag : MonoBehaviour {

	public List<string> TriggerTags = new List<string>();

	void OnTriggerEnter(Collider collided)
	{
		if(TriggerTags.Count > 0)
		{
			foreach(string TAG in TriggerTags)
			{
				if(collided.gameObject.CompareTag(TAG))
				{
					Destroy(collided.gameObject);
				}
			}
		}
	}
}
