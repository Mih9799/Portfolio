using UnityEngine;
using System.Collections;
using System.Collections.Generic;

[System.Serializable]
public class MultiTriggertrigger
{
	public Rigidbody TriggerZone;
	
	public float ActiveFadeTime = 0;
	
	public List<string> CorrectTags;
	
	public List<Rigidbody> CorrectObjects;
	
}
public class MultiSwitchTrigger : MonoBehaviour {


	public MultiSwitchBaseClass MySwitch;



	public MultiTriggertrigger MyTriggers;


	bool IsActivated = false;
	int InCount = 0;

	float MyFadeTimer = 0;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
		if(IsActivated && MyFadeTimer > 0)
		{
			MyFadeTimer -= Time.deltaTime;

			if(MyFadeTimer <= 0)
			{
				IsActivated = false;
				MySwitch.RemoveCount();
				MyFadeTimer = 0;
			}
		}
	}
	
	
	void OnTriggerEnter(Collider collided)
	{
		if(MyTriggers.CorrectTags.Count > 0)
		{
			foreach(string TAG in MyTriggers.CorrectTags)
			{
				if(collided.gameObject.CompareTag(TAG))
				{					
					if(!IsActivated && InCount == 0)
					{
						MySwitch.AddCount();
						IsActivated = true;
					}
					InCount++;
					break;
				}
			}
		}
		
		if(MyTriggers.CorrectObjects.Count > 0)
		{
			foreach(Rigidbody RR in MyTriggers.CorrectObjects)
			{
				if(collided.attachedRigidbody == RR)
				{				
					if(!IsActivated && InCount == 0)
					{
						MySwitch.AddCount();
						IsActivated = true;
					}
					InCount++;
					break;
				}
			}
		}
	}
	
	void OnTriggerExit(Collider collided)
	{
		if(MyTriggers.CorrectTags.Count > 0)
		{
			foreach(string TAG in MyTriggers.CorrectTags)
			{
				if(collided.gameObject.CompareTag(TAG))
				{	
					InCount--;
					if(InCount < 0)
					{
						InCount = 0;
					}
					if(IsActivated && InCount == 0)
					{
						//MySwitch.RemoveCount();
						//IsActivated = false;
						MyFadeTimer = MyTriggers.ActiveFadeTime;
					}
					break;
				}
			} 
		}

		
		if(MyTriggers.CorrectObjects.Count > 0)
		{
			foreach(Rigidbody RR in MyTriggers.CorrectObjects)
			{
				if(collided.attachedRigidbody == RR)
				{	
					InCount--;
					if(InCount < 0)
					{
						InCount = 0;
					}
					if(IsActivated && InCount == 0)
					{
						//MySwitch.RemoveCount();
						//IsActivated = false;
						MyFadeTimer = MyTriggers.ActiveFadeTime;
					}
					break;
				}
			}
		}

	}
}























