using UnityEngine;
using System.Collections;
using System.Collections.Generic;

[AddComponentMenu("AIE Scripts/Animation Call/Animation Trigger On Off")]
public class AnimationTriggerOnOff : MonoBehaviour
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
	
	public enum AnimationTypes
	{
		Play,
		Blend,
		CrossFade,
	};
	public AnimationTypes AnimationType = AnimationTriggerOnOff.AnimationTypes.Play;
	bool Activated = false;
	int ActiveCount = 0;
	
	public int MinimumCount = 1;

	public List<string> TriggerTags = new List<string>();
	public List<AnimationOject> Targets = new List<AnimationOject>();
	
	float DelayTimer = 0;
	bool AllDone = false;
	// Use this for initialization
	
	// Update is called once per frame
	void Update () 
	{
		if(Activated )
		{
			if(!AllDone)
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
							AObject.Target.GetComponent<Animation>()[AObject.AnimationName].speed = 1;
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
				}
			}
		}
		else if(!AllDone)
		{
			
			AllDone  = true;
			if(DelayTimer > 0)
			{
				DelayTimer -= Time.deltaTime;
			}
			foreach(AnimationOject AObject in Targets)
			{
				if(AObject.IsDone())
				{
					AllDone  = false;
					if(AObject.Delay > DelayTimer)
					{
						AObject.Target.GetComponent<Animation>()[AObject.AnimationName].speed *= -1;
						AObject.Target.GetComponent<Animation>()[AObject.AnimationName].enabled = true;
						if(AObject.Target.GetComponent<Animation>()[AObject.AnimationName].normalizedTime == 0)
						{
							AObject.Target.GetComponent<Animation>()[AObject.AnimationName].normalizedTime = 1;
						}
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
						AObject.UnDone();
					}
				}			
			}
		}
		
	
	}
	
	void OnTriggerEnter(Collider collided)
	{
		if(!Activated)
		{
			if(TriggerTags.Count > 0)
			{
				foreach(string TAG in TriggerTags)
				{
					if(collided.gameObject.CompareTag(TAG))
					{
						ActiveCount++;
						if(ActiveCount >= MinimumCount)
						{
							Activated = true;
							AllDone  = false;
						}
						break;
					}
				}
			}
			else
			{
				ActiveCount++;
				if(ActiveCount >= MinimumCount)
				{
					Activated = true;
					AllDone  = false;
				}
			}
		}
		else
		{
			if(TriggerTags.Count > 0)
			{
				foreach(string TAG in TriggerTags)
				{
					if(collided.gameObject.CompareTag(TAG))
					{
						ActiveCount++;
						break;
					}
				}
			}
			else
			{
				ActiveCount++;
			}
		}
	}
	
	void OnTriggerExit(Collider collided)
	{
		if(Activated)
		{
			if(TriggerTags.Count > 0)
			{
				foreach(string TAG in TriggerTags)
				{
					if(collided.gameObject.CompareTag(TAG))
					{
						ActiveCount--;
						if(ActiveCount < MinimumCount)
						{
							Activated = false;
							AllDone  = false;
						}
						break;
					}
				}
			}
			else
			{
				ActiveCount--;
				if(ActiveCount < MinimumCount)
				{
					Activated = false;
					AllDone  = false;
				}
			}
		}
	}
}
