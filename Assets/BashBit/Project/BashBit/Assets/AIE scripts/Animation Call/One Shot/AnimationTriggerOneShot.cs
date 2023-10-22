using UnityEngine;
using System.Collections;
using System.Collections.Generic;

[AddComponentMenu("AIE Scripts/Animation Call/Animation Trigger One Shot")]
public class AnimationTriggerOneShot : MonoBehaviour 
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
	};
	
	public enum AnimationTypes
	{
		Play,
		Blend,
		CrossFade,
	};
	public AnimationTypes AnimationType = AnimationTriggerOneShot.AnimationTypes.Play;
	public bool TriggerOnEnter = false;
	bool Activated = false;
	public bool TriggerOnExit = false;


	public int MinimumCount = 1;
	int CurrentCount = 0;

	public List<string> TriggerTags = new List<string>();	
	public List<AnimationOject> Targets = new List<AnimationOject>();
	
	float DelayTimer = 0;
	bool AllDone = false;
	
	// Update is called once per frame
	void Update () 
	{
		if(Activated && !AllDone)
		{			
			DelayTimer += Time.deltaTime;
			AllDone  = true;
			
			foreach(AnimationOject AObject in Targets)
			{
				if(!AObject.IsDone())
				{
					AllDone  = false;
					if(AObject.Delay < DelayTimer)
					{
						switch(AnimationType)
							{
							case AnimationTypes.Play:
								AObject.Target.GetComponent<Animation>().Play(AObject.AnimationName);
								break;
							case AnimationTypes.Blend:
								AObject.Target.GetComponent<Animation>().Blend(AObject.AnimationName);
								break;
							case AnimationTypes.CrossFade:
								AObject.Target.GetComponent<Animation>().CrossFade(AObject.AnimationName);
								break;
							}
						AObject.Done();
					}
				}
				else
				{
				}
			}
		}
		
	
	}
	
	void OnTriggerEnter(Collider collided)
	{
		if(TriggerOnEnter && !Activated)
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
							Activated = true;
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
					Activated = true;
				}
			}
		}
	}
	
	void OnTriggerExit(Collider collided)
	{
		if(TriggerOnExit && !Activated)
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
							Activated = true;
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
					Activated = true;
				}
			}
		}
	}
}
