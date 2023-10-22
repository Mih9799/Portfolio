using UnityEngine;
using System.Collections;
using System.Collections.Generic;

[AddComponentMenu("AIE Scripts/Animation Call/Animation Click On Off")]
public class AnimationClickOnOff : MonoBehaviour {
	
	
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
	public enum eClickType
	{
		DownUp,
		EnterExit,
		ClickOnClickOff,
	};
	public AnimationTypes AnimationType = AnimationClickOnOff.AnimationTypes.Play;
	bool Activated = false;
	int ActiveCount = 0;

	public eClickType ClickType = eClickType.ClickOnClickOff;



	public List<AnimationOject> Targets = new List<AnimationOject>();
	
	float DelayTimer = 0;
	bool AllDone = false;
	// Use this for initialization

	
	public bool UseDistance = false;
	public float DistanceToActivate = 10;
	Transform PlayerPosition;
	
	void Start()
	{
		PlayerPosition = GameObject.FindWithTag("Player").transform;
	}
	// Update is called once per frame
	void Update () 
	{
		
		if(!UseDistance || 
		   Vector3.Distance(PlayerPosition.position,transform.position) < DistanceToActivate)
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
	}
	
	void OnMouseEnter()
	{
		if(ClickType == eClickType.EnterExit)
		{
			if(!Activated)
			{
				Activated = true;
				AllDone  = false;
				ActiveCount++;
			}
			else
			{
				ActiveCount++;
			}
		}
	}
	
	void OnMouseExit()
	{
		if(ClickType == eClickType.EnterExit)
		{
			if(Activated)
			{
				ActiveCount--;
				if(ActiveCount <=0)
				{
					Activated = false;
					AllDone  = false;
				}
			}
		}
	}
	
	void OnMouseDown()
	{
		if(ClickType == eClickType.DownUp)
		{
			if(!Activated)
			{
				Activated = true;
				AllDone  = false;
				ActiveCount++;
			}
			else
			{
				ActiveCount++;
			}
		}
	}
	
	void OnMouseUp()
	{
		if(ClickType == eClickType.DownUp)
		{
			if(Activated)
			{
				ActiveCount--;
				if(ActiveCount <=0)
				{
					Activated = false;
					AllDone  = false;
				}
			}
		}
	}
	
	void OnMouseUpAsButton()
	{
		if(ClickType == eClickType.ClickOnClickOff)
		{
			if(!Activated)
			{
				Activated = true;
				AllDone  = false;
				ActiveCount++;
			}
			else
			{
				ActiveCount--;
				if(ActiveCount <=0)
				{
					Activated = false;
					AllDone  = false;
				}
			}
		}
	}
}















