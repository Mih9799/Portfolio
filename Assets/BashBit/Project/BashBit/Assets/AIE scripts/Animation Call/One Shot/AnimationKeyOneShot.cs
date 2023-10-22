using UnityEngine;
using System.Collections;
using System.Collections.Generic;

[AddComponentMenu("AIE Scripts/Animation Call/Animation Key One Shot")]
public class AnimationKeyOneShot : MonoBehaviour 
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
	public AnimationTypes AnimationType = AnimationKeyOneShot.AnimationTypes.Play;

	public bool AnyKey = false;
	
	public bool Usekey = true;
	public KeyCode ActivationKey = KeyCode.Return;
	
	public bool UseButton = false;
	public string ActivationButton = "Fire1";

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
		
		if(!UseDistance || 
		   Vector3.Distance(PlayerPosition.position,transform.position) < DistanceToActivate)
		{
			if((Usekey && Input.GetKeyDown(ActivationKey)) ||
			   (AnyKey && Input.anyKeyDown) ||
			   (UseButton && Input.GetButtonDown(ActivationButton)))
			{
				Activated = true;
			}	
		}
	}
}























