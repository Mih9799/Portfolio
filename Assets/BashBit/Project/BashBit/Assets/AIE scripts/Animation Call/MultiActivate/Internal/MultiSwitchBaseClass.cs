using UnityEngine;
using System.Collections;
using System.Collections.Generic;


public class MultiSwitchBaseClass : MonoBehaviour 
{
	[System.Serializable]
	public class AnimationOject
	{
		public GameObject Target;
		public string AnimationName;
		public float Delay;
		
		bool DelayDone = false; 
		public bool IsDone()
		{
			return DelayDone;
		}
		public void Done()
		{
			DelayDone = true;
		}
		public void UnDone()
		{
			DelayDone = false;
		}
	};
	
	
	public List<MultiTriggertrigger> TheTriggers;
	
	protected bool AllActivated = false;
	protected int ActivatedCount = 0;
	
	protected float DelayTimer = 0;
	protected bool AllDone = false;
	
	public List<AnimationOject> TheAnimations;
	
	
	// Use this for initialization
	void Start ()
	{		
		MultiSwitchTrigger TEMP;
		foreach(MultiTriggertrigger MM in TheTriggers)
		{
			TEMP = MM.TriggerZone.gameObject.AddComponent<MultiSwitchTrigger>();
			TEMP.MySwitch = this;
			TEMP.MyTriggers = MM;
		}		
	}

	
	public void AddCount()
	{
		ActivatedCount++;
		if(ActivatedCount >= TheTriggers.Count)
		{
			AllActivated = true;
			AllDone = false;
		}
	}
	
	
	public void RemoveCount()
	{	
		ActivatedCount--;
		if(AllActivated && ActivatedCount < TheTriggers.Count)
		{
			AllActivated = false;
			AllDone = false;
		}
	}
}
































