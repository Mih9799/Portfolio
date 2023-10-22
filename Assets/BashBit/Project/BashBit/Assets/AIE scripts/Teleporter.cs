using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Teleporter : MonoBehaviour {
	public GameObject SpawnPoint;
	public List<string> TriggerTags = new List<string>();
	
	void OnTriggerEnter(Collider cObject)
	{
		if(TriggerTags.Count > 0)
		{
			foreach(string TAG in TriggerTags)
			{
				if(cObject.gameObject.CompareTag(TAG))
				{
					cObject.gameObject.GetComponent<Transform>().transform.position = SpawnPoint.GetComponent<Transform>().transform.position;
					cObject.gameObject.GetComponent<Transform>().transform.rotation = cObject.gameObject.GetComponent<Transform>().transform.rotation;
				}
			}
		}
	}
}
