using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ChangeLevel : MonoBehaviour {

	public List<string> TriggerTags = new List<string>();
	public string NextLevel;

	public int MinimumCount = 1;
	int CurrentCount = 0;

	bool Activated = false;
	
	void OnTriggerEnter(Collider collided)
	{
		if(TriggerTags.Count > 0)
		{
			foreach(string TAG in TriggerTags)
			{
				if(collided.gameObject.CompareTag(TAG))
				{
					CurrentCount++;
					if(CurrentCount  >= MinimumCount)
					{
						Application.LoadLevel(NextLevel);
					}
					break;
				}
			}
		}
		else
		{
			CurrentCount++;
			if(CurrentCount  >= MinimumCount)
			{
				Application.LoadLevel(NextLevel);
			}
		}
	}
}
