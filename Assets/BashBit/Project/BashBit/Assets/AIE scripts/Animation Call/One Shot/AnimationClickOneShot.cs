using UnityEngine;
using System.Collections;
using System.Collections.Generic;

[AddComponentMenu("AIE Scripts/Animation Call/Animation Click One Shot")]
public class AnimationClickOneShot : MonoBehaviour
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
	public AnimationTypes AnimationType = AnimationClickOneShot.AnimationTypes.Play;

	public bool TriggerOnMouseDown = false;
	//public bool TriggerOnMouseUp = false;
	public bool TriggerOnMouseEnter = false;
	public bool TriggerOnMouseExit = false;
	public bool TriggerOnMouseClick = true;

	
	bool Activated = false;

	public List<AnimationOject> Targets = new List<AnimationOject>();
	
	float DelayTimer = 0;
	bool AllDone = false;

	
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
		
		
	}
	
	void OnMouseDown()
	{
		if(TriggerOnMouseDown && !Activated)
		{
			Activated = true;
		}
	}
	
	
//	void OnMouseUp() 
//	{
//		if(TriggerOnMouseUp && !Activated)
//		{
//			Activated = true;
//		}
//	}
	
	
	void OnMouseUpAsButton()
	{
		if(TriggerOnMouseClick && !Activated)
		{
			Activated = true;
		}
	}

	void OnMouseEnter()
	{
		if(TriggerOnMouseEnter && !Activated)
		{
				Activated = true;
		}
	}
	
	void OnMouseExit()
	{
		if(TriggerOnMouseExit && !Activated)
		{
				Activated = true;
		}
	}
}
